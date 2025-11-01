namespace SettlementSurvivalOptimizer.Models;

/// <summary>
/// Represents a production building type
/// </summary>
public class Building
{
    public string Name { get; set; } = string.Empty;
    public int MaxWorkers { get; set; }
    public List<Recipe> Recipes { get; set; } = new();
    
    /// <summary>
    /// Efficiency penalty for partial staffing (0.0 to 1.0)
    /// E.g., if 0.8, then 50% staffing = 40% production
    /// </summary>
    public double PartialStaffingEfficiency { get; set; } = 0.9;
    
    /// <summary>
    /// Maintenance cost or upkeep (future feature)
    /// </summary>
    public double MaintenanceCost { get; set; }
}
