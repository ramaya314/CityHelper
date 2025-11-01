using System.Text.Json;
using SettlementSurvivalOptimizer.Models;

namespace SettlementSurvivalOptimizer.Services;

/// <summary>
/// Loads production chain data from JSON configuration files
/// </summary>
public class DataLoader
{
    private readonly string _dataPath;

    public DataLoader(string dataPath = "Data")
    {
        _dataPath = dataPath;
    }

    /// <summary>
    /// Load complete production chain from JSON files
    /// </summary>
    public ProductionChain LoadProductionChain()
    {
        var chain = new ProductionChain();
        
        // Load buildings
        var buildingsPath = Path.Combine(_dataPath, "buildings.json");
        if (File.Exists(buildingsPath))
        {
            var json = File.ReadAllText(buildingsPath);
            var buildings = JsonSerializer.Deserialize<List<Building>>(json, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            });
            
            if (buildings != null)
            {
                foreach (var building in buildings)
                {
                    chain.Buildings[building.Name] = building;
                    
                    // Build resource producer/consumer maps
                    foreach (var recipe in building.Recipes)
                    {
                        foreach (var output in recipe.Outputs.Keys)
                        {
                            if (!chain.ResourceProducers.ContainsKey(output))
                                chain.ResourceProducers[output] = new List<string>();
                            
                            if (!chain.ResourceProducers[output].Contains(building.Name))
                                chain.ResourceProducers[output].Add(building.Name);
                        }
                        
                        foreach (var input in recipe.Inputs.Keys)
                        {
                            if (!chain.ResourceConsumers.ContainsKey(input))
                                chain.ResourceConsumers[input] = new List<string>();
                            
                            if (!chain.ResourceConsumers[input].Contains(building.Name))
                                chain.ResourceConsumers[input].Add(building.Name);
                        }
                    }
                }
            }
        }
        
        return chain;
    }

    /// <summary>
    /// Load resource data from JSON
    /// </summary>
    public List<Resource> LoadResources()
    {
        var resourcesPath = Path.Combine(_dataPath, "resources.json");
        if (File.Exists(resourcesPath))
        {
            var json = File.ReadAllText(resourcesPath);
            return JsonSerializer.Deserialize<List<Resource>>(json, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            }) ?? new List<Resource>();
        }
        
        return new List<Resource>();
    }

    /// <summary>
    /// Save resource data to JSON
    /// </summary>
    public void SaveResources(List<Resource> resources)
    {
        var resourcesPath = Path.Combine(_dataPath, "resources.json");
        var json = JsonSerializer.Serialize(resources, new JsonSerializerOptions 
        { 
            WriteIndented = true 
        });
        File.WriteAllText(resourcesPath, json);
    }
}
