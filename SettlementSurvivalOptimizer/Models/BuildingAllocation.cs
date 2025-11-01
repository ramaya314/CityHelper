namespace SettlementSurvivalOptimizer.Models;

/// <summary>
/// Represents the calculated optimal allocation for a building type
/// </summary>
public class BuildingAllocation
{
    public string BuildingName { get; set; } = string.Empty;
    public string RecipeName { get; set; } = string.Empty;
    
    /// <summary>
    /// Number of buildings needed
    /// </summary>
    public int BuildingCount { get; set; }
    
    /// <summary>
    /// Workers per building (can be fractional for average)
    /// </summary>
    public double WorkersPerBuilding { get; set; }
    
    /// <summary>
    /// Total workers needed across all buildings
    /// </summary>
    public int TotalWorkers { get; set; }
    
    /// <summary>
    /// Expected annual production
    /// </summary>
    public Dictionary<string, double> ExpectedProduction { get; set; } = new();
    
    /// <summary>
    /// Expected annual consumption
    /// </summary>
    public Dictionary<string, double> ExpectedConsumption { get; set; } = new();
    
    /// <summary>
    /// Efficiency rating (0.0 to 1.0)
    /// </summary>
    public double Efficiency { get; set; }
}
