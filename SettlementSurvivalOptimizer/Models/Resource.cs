namespace SettlementSurvivalOptimizer.Models;

/// <summary>
/// Represents a resource or product in the game
/// </summary>
public class Resource
{
    public string Name { get; set; } = string.Empty;
    public double Stock { get; set; }
    public double UsedTotal { get; set; }
    public double UsedLastYear { get; set; }
    public double ProducedTotal { get; set; }
    public double ProducedLastYear { get; set; }
    
    /// <summary>
    /// Target surplus (+) or deficit (-) for this resource per year
    /// </summary>
    public double TargetSurplus { get; set; }
    
    /// <summary>
    /// Is this a final product we're trying to optimize for?
    /// </summary>
    public bool IsFinalProduct { get; set; }
}
