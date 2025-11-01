using System;
using System.Collections.Generic;
using System.Linq;
using SettlementSurvivalOptimizer.UI.Models;

namespace SettlementSurvivalOptimizer.UI.Services;

public class OptimizationService
{
    public OptimizationResult Optimize(
        List<BuildingEntity> buildings,
        List<ResourceEntity> resources,
        int population)
    {
        var result = new OptimizationResult
        {
            TotalPopulation = population
        };

        // Build production chain
        var chain = BuildProductionChain(buildings);
        
        // Calculate demands
        var finalProducts = resources.Where(r => r.IsFinalProduct).ToList();
        var demands = CalculateResourceDemands(finalProducts, chain, result.Warnings);
        
        // Allocate buildings and workers
        var allocations = AllocateProductionCapacity(demands, buildings, population, result.Warnings);
        result.BuildingAllocations = allocations;
        result.TotalWorkersNeeded = allocations.Sum(a => a.TotalWorkers);
        
        // Calculate resource balance
        result.ResourceBalance = CalculateResourceBalance(allocations, finalProducts);
        
        // Calculate efficiency
        result.OverallEfficiency = CalculateOverallEfficiency(allocations, population);
        
        return result;
    }

    private Dictionary<string, List<(BuildingEntity building, RecipeEntity recipe)>> BuildProductionChain(
        List<BuildingEntity> buildings)
    {
        var producers = new Dictionary<string, List<(BuildingEntity, RecipeEntity)>>();
        
        foreach (var building in buildings)
        {
            foreach (var recipe in building.Recipes)
            {
                foreach (var output in recipe.Outputs)
                {
                    if (!producers.ContainsKey(output.ResourceName))
                        producers[output.ResourceName] = new();
                    
                    producers[output.ResourceName].Add((building, recipe));
                }
            }
        }
        
        return producers;
    }

    private Dictionary<string, double> CalculateResourceDemands(
        List<ResourceEntity> finalProducts,
        Dictionary<string, List<(BuildingEntity building, RecipeEntity recipe)>> producers,
        List<string> warnings)
    {
        var demands = new Dictionary<string, double>();
        var processed = new HashSet<string>();
        var queue = new Queue<string>();

        foreach (var product in finalProducts)
        {
            double required = product.UsedLastYear + product.TargetSurplus;
            
            if (product.Stock < product.UsedLastYear * 0.25)
            {
                double deficit = (product.UsedLastYear * 0.25) - product.Stock;
                required += deficit;
                warnings.Add($"Low stock: {product.Name} has only {product.Stock:F0} units. Adding {deficit:F0} to production.");
            }
            
            demands[product.Name] = required;
            queue.Enqueue(product.Name);
        }

        while (queue.Count > 0)
        {
            var resourceName = queue.Dequeue();
            
            if (processed.Contains(resourceName))
                continue;
            
            processed.Add(resourceName);

            if (!producers.ContainsKey(resourceName))
            {
                if (finalProducts.Any(p => p.Name == resourceName))
                    warnings.Add($"No production method for '{resourceName}'. Must be sourced externally.");
                continue;
            }

            var (_, recipe) = producers[resourceName].First();
            var output = recipe.Outputs.First(o => o.ResourceName == resourceName);
            double batchesNeeded = demands[resourceName] / output.Quantity;

            foreach (var input in recipe.Inputs)
            {
                double inputDemand = input.Quantity * batchesNeeded;
                
                if (demands.ContainsKey(input.ResourceName))
                    demands[input.ResourceName] += inputDemand;
                else
                {
                    demands[input.ResourceName] = inputDemand;
                    queue.Enqueue(input.ResourceName);
                }
            }
        }

        return demands;
    }

    private List<BuildingAllocationResult> AllocateProductionCapacity(
        Dictionary<string, double> demands,
        List<BuildingEntity> buildings,
        int population,
        List<string> warnings)
    {
        var allocations = new List<BuildingAllocationResult>();
        var remainingPopulation = population;

        var buildingProduction = new Dictionary<BuildingEntity, (RecipeEntity recipe, double demand)>();

        foreach (var demand in demands)
        {
            var building = buildings.FirstOrDefault(b => 
                b.Recipes.Any(r => r.Outputs.Any(o => o.ResourceName == demand.Key)));
            
            if (building == null) continue;

            var recipe = building.Recipes.First(r => 
                r.Outputs.Any(o => o.ResourceName == demand.Key));

            if (!buildingProduction.ContainsKey(building))
            {
                buildingProduction[building] = (recipe, demand.Value);
            }
        }

        foreach (var item in buildingProduction)
        {
            var building = item.Key;
            var recipe = item.Value.recipe;
            var demand = item.Value.demand;

            double workersNeeded = demand / recipe.ProductionRatePerWorkerPerYear;
            int buildingCount = (int)Math.Ceiling(workersNeeded / building.MaxWorkers);
            double avgWorkersPerBuilding = workersNeeded / buildingCount;
            int totalWorkers = (int)Math.Ceiling(workersNeeded);

            if (totalWorkers > remainingPopulation)
            {
                totalWorkers = remainingPopulation;
                warnings.Add($"Population limit: {building.Name} reduced to {totalWorkers} workers.");
            }

            remainingPopulation -= totalWorkers;

            double efficiency = (avgWorkersPerBuilding / building.MaxWorkers) * building.PartialStaffingEfficiency;
            if (avgWorkersPerBuilding >= building.MaxWorkers * 0.95)
                efficiency = 1.0;

            double actualProduction = totalWorkers * recipe.ProductionRatePerWorkerPerYear * efficiency;

            var expectedProduction = new Dictionary<string, double>();
            var expectedConsumption = new Dictionary<string, double>();

            foreach (var output in recipe.Outputs)
            {
                expectedProduction[output.ResourceName] = 
                    actualProduction * (output.Quantity / recipe.Outputs.First().Quantity);
            }

            foreach (var input in recipe.Inputs)
            {
                expectedConsumption[input.ResourceName] = 
                    actualProduction * (input.Quantity / recipe.Outputs.First().Quantity);
            }

            allocations.Add(new BuildingAllocationResult
            {
                BuildingName = building.Name,
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

    private Dictionary<string, double> CalculateResourceBalance(
        List<BuildingAllocationResult> allocations,
        List<ResourceEntity> finalProducts)
    {
        var balance = new Dictionary<string, double>();

        foreach (var allocation in allocations)
        {
            foreach (var prod in allocation.ExpectedProduction)
            {
                if (!balance.ContainsKey(prod.Key))
                    balance[prod.Key] = 0;
                balance[prod.Key] += prod.Value;
            }

            foreach (var cons in allocation.ExpectedConsumption)
            {
                if (!balance.ContainsKey(cons.Key))
                    balance[cons.Key] = 0;
                balance[cons.Key] -= cons.Value;
            }
        }

        foreach (var product in finalProducts)
        {
            if (!balance.ContainsKey(product.Name))
                balance[product.Name] = 0;
            balance[product.Name] -= product.UsedLastYear;
        }

        return balance;
    }

    private double CalculateOverallEfficiency(
        List<BuildingAllocationResult> allocations,
        int population)
    {
        if (allocations.Count == 0 || population == 0)
            return 0.0;

        double totalWeightedEff = allocations.Sum(a => a.Efficiency * a.TotalWorkers);
        int totalWorkers = allocations.Sum(a => a.TotalWorkers);

        if (totalWorkers == 0)
            return 0.0;

        double avgEff = totalWeightedEff / totalWorkers;
        double popUtil = Math.Min(1.0, (double)totalWorkers / population);

        return avgEff * popUtil;
    }
}

public class OptimizationResult
{
    public List<BuildingAllocationResult> BuildingAllocations { get; set; } = new();
    public int TotalWorkersNeeded { get; set; }
    public int TotalPopulation { get; set; }
    public Dictionary<string, double> ResourceBalance { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public double OverallEfficiency { get; set; }
}

public class BuildingAllocationResult
{
    public string BuildingName { get; set; } = string.Empty;
    public string RecipeName { get; set; } = string.Empty;
    public int BuildingCount { get; set; }
    public double WorkersPerBuilding { get; set; }
    public int TotalWorkers { get; set; }
    public Dictionary<string, double> ExpectedProduction { get; set; } = new();
    public Dictionary<string, double> ExpectedConsumption { get; set; } = new();
    public double Efficiency { get; set; }
}
