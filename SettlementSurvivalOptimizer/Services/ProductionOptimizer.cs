using SettlementSurvivalOptimizer.Models;

namespace SettlementSurvivalOptimizer.Services;

/// <summary>
/// Core optimization engine that calculates optimal production and worker allocation
/// using backward chaining from target products
/// </summary>
public class ProductionOptimizer
{
    private readonly ProductionChain _productionChain;

    public ProductionOptimizer(ProductionChain productionChain)
    {
        _productionChain = productionChain;
    }

    /// <summary>
    /// Main optimization method: calculates building and worker allocations to meet targets
    /// </summary>
    public OptimizationResult Optimize(List<Resource> targetProducts, int availablePopulation)
    {
        var result = new OptimizationResult();
        
        // Step 1: Calculate total demand for each resource working backward
        var resourceDemands = CalculateResourceDemands(targetProducts, result.Warnings);
        
        // Step 2: Allocate buildings and workers to meet demands
        var allocations = AllocateProductionCapacity(resourceDemands, availablePopulation, result.Warnings);
        
        result.BuildingAllocations = allocations;
        result.TotalWorkersNeeded = allocations.Sum(a => a.TotalWorkers);
        
        // Step 3: Calculate projected resource balance
        result.ResourceBalance = CalculateResourceBalance(allocations, targetProducts);
        
        // Step 4: Calculate overall efficiency
        result.OverallEfficiency = CalculateOverallEfficiency(allocations, availablePopulation);
        
        // Step 5: Add warnings for population constraints
        if (result.TotalWorkersNeeded > availablePopulation)
        {
            result.Warnings.Add($"Warning: Optimization requires {result.TotalWorkersNeeded} workers but only {availablePopulation} available. Production targets may not be met.");
        }
        
        return result;
    }

    /// <summary>
    /// Calculate total demand for each resource by working backward from final products
    /// through the entire production chain
    /// </summary>
    private Dictionary<string, double> CalculateResourceDemands(
        List<Resource> targetProducts, 
        List<string> warnings)
    {
        var demands = new Dictionary<string, double>();
        var processed = new HashSet<string>();
        
        // Start with final products and their target production (considering surplus)
        foreach (var product in targetProducts.Where(p => p.IsFinalProduct))
        {
            // Calculate required production: consumption + target surplus
            double requiredProduction = product.UsedLastYear + product.TargetSurplus;
            
            // Account for current stock if we're running low
            if (product.Stock < product.UsedLastYear * 0.25) // Less than 3 months stock
            {
                double stockDeficit = (product.UsedLastYear * 0.25) - product.Stock;
                requiredProduction += stockDeficit;
                warnings.Add($"Low stock warning: {product.Name} has only {product.Stock:F0} units (less than 25% of annual usage). Adding {stockDeficit:F0} to production target.");
            }
            
            demands[product.Name] = requiredProduction;
        }
        
        // Recursively calculate intermediate resource demands
        var queue = new Queue<string>(demands.Keys);
        
        while (queue.Count > 0)
        {
            var resourceName = queue.Dequeue();
            
            if (processed.Contains(resourceName))
                continue;
                
            processed.Add(resourceName);
            
            // Find buildings that produce this resource
            if (!_productionChain.ResourceProducers.ContainsKey(resourceName))
            {
                warnings.Add($"Warning: No production method found for '{resourceName}'. This must be sourced externally.");
                continue;
            }
            
            var producers = _productionChain.ResourceProducers[resourceName];
            
            // For now, use the first producer found (can be enhanced with multi-producer optimization)
            var buildingName = producers.First();
            var building = _productionChain.Buildings[buildingName];
            var recipe = building.Recipes.FirstOrDefault(r => r.Outputs.ContainsKey(resourceName));
            
            if (recipe == null)
                continue;
            
            // Calculate how many batches we need to produce the required amount
            double outputPerBatch = recipe.Outputs[resourceName];
            double batchesNeeded = demands[resourceName] / outputPerBatch;
            
            // Add demand for input resources
            foreach (var input in recipe.Inputs)
            {
                double inputDemand = input.Value * batchesNeeded;
                
                if (demands.ContainsKey(input.Key))
                    demands[input.Key] += inputDemand;
                else
                {
                    demands[input.Key] = inputDemand;
                    queue.Enqueue(input.Key);
                }
            }
        }
        
        return demands;
    }

    /// <summary>
    /// Allocate buildings and workers to meet resource demands
    /// </summary>
    private List<BuildingAllocation> AllocateProductionCapacity(
        Dictionary<string, double> resourceDemands,
        int availablePopulation,
        List<string> warnings)
    {
        var allocations = new List<BuildingAllocation>();
        var remainingPopulation = availablePopulation;
        
        // Group demands by building type
        var buildingDemands = new Dictionary<string, (Recipe recipe, double demand)>();
        
        foreach (var demand in resourceDemands)
        {
            if (!_productionChain.ResourceProducers.ContainsKey(demand.Key))
                continue;
                
            var buildingName = _productionChain.ResourceProducers[demand.Key].First();
            var building = _productionChain.Buildings[buildingName];
            var recipe = building.Recipes.FirstOrDefault(r => r.Outputs.ContainsKey(demand.Key));
            
            if (recipe != null)
            {
                // Calculate production demand (not consumption demand)
                double outputPerBatch = recipe.Outputs[demand.Key];
                
                if (!buildingDemands.ContainsKey(buildingName))
                {
                    buildingDemands[buildingName] = (recipe, demand.Value);
                }
                else
                {
                    // Accumulate if multiple resources from same building
                    var existing = buildingDemands[buildingName];
                    buildingDemands[buildingName] = (existing.recipe, existing.demand + demand.Value);
                }
            }
        }
        
        // Allocate buildings and workers
        foreach (var demand in buildingDemands)
        {
            var buildingName = demand.Key;
            var recipe = demand.Value.recipe;
            var totalDemand = demand.Value.demand;
            var building = _productionChain.Buildings[buildingName];
            
            // Calculate total production needed (considering primary output)
            var primaryOutput = recipe.Outputs.First();
            double productionNeeded = totalDemand;
            
            // Calculate workers needed based on production rate
            // Assuming production rate is per worker per year
            double workersNeeded = productionNeeded / recipe.ProductionRatePerWorkerPerYear;
            
            // Calculate building count and workers per building
            int buildingCount = (int)Math.Ceiling(workersNeeded / building.MaxWorkers);
            double avgWorkersPerBuilding = workersNeeded / buildingCount;
            int totalWorkers = (int)Math.Ceiling(workersNeeded);
            
            // Adjust for population constraints
            if (totalWorkers > remainingPopulation)
            {
                totalWorkers = remainingPopulation;
                warnings.Add($"Population constraint: {buildingName} allocation reduced to {totalWorkers} workers (needed {workersNeeded:F1})");
            }
            
            remainingPopulation -= totalWorkers;
            
            // Calculate efficiency
            double efficiency = (avgWorkersPerBuilding / building.MaxWorkers) * building.PartialStaffingEfficiency;
            if (avgWorkersPerBuilding == building.MaxWorkers)
                efficiency = 1.0;
            
            // Calculate expected production and consumption
            var expectedProduction = new Dictionary<string, double>();
            var expectedConsumption = new Dictionary<string, double>();
            
            double actualProduction = totalWorkers * recipe.ProductionRatePerWorkerPerYear * efficiency;
            
            foreach (var output in recipe.Outputs)
            {
                expectedProduction[output.Key] = actualProduction * (output.Value / recipe.Outputs.First().Value);
            }
            
            foreach (var input in recipe.Inputs)
            {
                expectedConsumption[input.Key] = actualProduction * (input.Value / recipe.Outputs.First().Value);
            }
            
            allocations.Add(new BuildingAllocation
            {
                BuildingName = buildingName,
                RecipeName = recipe.Name,
                BuildingCount = buildingCount,
                WorkersPerBuilding = avgWorkersPerBuilding,
                TotalWorkers = totalWorkers,
                ExpectedProduction = expectedProduction,
                ExpectedConsumption = expectedConsumption,
                Efficiency = efficiency
            });
        }
        
        return allocations;
    }

    /// <summary>
    /// Calculate net resource balance (production - consumption)
    /// </summary>
    private Dictionary<string, double> CalculateResourceBalance(
        List<BuildingAllocation> allocations,
        List<Resource> targetProducts)
    {
        var balance = new Dictionary<string, double>();
        
        // Sum all production
        foreach (var allocation in allocations)
        {
            foreach (var production in allocation.ExpectedProduction)
            {
                if (balance.ContainsKey(production.Key))
                    balance[production.Key] += production.Value;
                else
                    balance[production.Key] = production.Value;
            }
        }
        
        // Subtract all consumption (including final product usage)
        foreach (var allocation in allocations)
        {
            foreach (var consumption in allocation.ExpectedConsumption)
            {
                if (balance.ContainsKey(consumption.Key))
                    balance[consumption.Key] -= consumption.Value;
                else
                    balance[consumption.Key] = -consumption.Value;
            }
        }
        
        // Subtract final product consumption
        foreach (var product in targetProducts.Where(p => p.IsFinalProduct))
        {
            if (balance.ContainsKey(product.Name))
                balance[product.Name] -= product.UsedLastYear;
            else
                balance[product.Name] = -product.UsedLastYear;
        }
        
        return balance;
    }

    /// <summary>
    /// Calculate overall efficiency score
    /// </summary>
    private double CalculateOverallEfficiency(List<BuildingAllocation> allocations, int availablePopulation)
    {
        if (allocations.Count == 0)
            return 0.0;
        
        // Average efficiency across all buildings weighted by worker count
        double totalWeightedEfficiency = allocations.Sum(a => a.Efficiency * a.TotalWorkers);
        int totalWorkers = allocations.Sum(a => a.TotalWorkers);
        
        if (totalWorkers == 0)
            return 0.0;
        
        double avgEfficiency = totalWeightedEfficiency / totalWorkers;
        
        // Penalize if we can't use all available population
        double populationUtilization = Math.Min(1.0, (double)totalWorkers / availablePopulation);
        
        return avgEfficiency * populationUtilization;
    }
}
