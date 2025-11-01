namespace SettlementSurvivalOptimizer.Models;

/// <summary>
/// Represents the entire production chain graph
/// </summary>
public class ProductionChain
{
    public Dictionary<string, Building> Buildings { get; set; } = new();
    public Dictionary<string, Resource> Resources { get; set; } = new();
    
    /// <summary>
    /// Maps resource name to buildings that produce it
    /// </summary>
    public Dictionary<string, List<string>> ResourceProducers { get; set; } = new();
    
    /// <summary>
    /// Maps resource name to buildings that consume it
    /// </summary>
    public Dictionary<string, List<string>> ResourceConsumers { get; set; } = new();
}
