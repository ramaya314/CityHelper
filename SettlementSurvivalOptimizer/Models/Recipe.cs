namespace SettlementSurvivalOptimizer.Models;

/// <summary>
/// Represents a production recipe: inputs -> outputs
/// </summary>
public class Recipe
{
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, double> Inputs { get; set; } = new();
    public Dictionary<string, double> Outputs { get; set; } = new();
    
    /// <summary>
    /// Production rate per worker per year
    /// </summary>
    public double ProductionRatePerWorkerPerYear { get; set; }
    
    /// <summary>
    /// Time to produce one batch (in game days)
    /// </summary>
    public double ProductionTimeInDays { get; set; }
}
