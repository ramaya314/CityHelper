namespace SettlementSurvivalOptimizer.Models;

/// <summary>
/// Complete optimization result with all allocations
/// </summary>
public class OptimizationResult
{
    public List<BuildingAllocation> BuildingAllocations { get; set; } = new();
    
    /// <summary>
    /// Total workers needed across all production
    /// </summary>
    public int TotalWorkersNeeded { get; set; }
    
    /// <summary>
    /// Projected annual resource balance (positive = surplus, negative = deficit)
    /// </summary>
    public Dictionary<string, double> ResourceBalance { get; set; } = new();
    
    /// <summary>
    /// Warnings or issues found during optimization
    /// </summary>
    public List<string> Warnings { get; set; } = new();
    
    /// <summary>
    /// Overall efficiency score (0.0 to 1.0)
    /// </summary>
    public double OverallEfficiency { get; set; }
}
