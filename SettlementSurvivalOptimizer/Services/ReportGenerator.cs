using System.Text;
using SettlementSurvivalOptimizer.Models;

namespace SettlementSurvivalOptimizer.Services;

/// <summary>
/// Generates human-readable reports from optimization results
/// </summary>
public class ReportGenerator
{
    public string GenerateDetailedReport(OptimizationResult result, List<Resource> targetProducts, int population)
    {
        var sb = new StringBuilder();
        
        sb.AppendLine("═══════════════════════════════════════════════════════════════════");
        sb.AppendLine("     SETTLEMENT SURVIVAL - PRODUCTION OPTIMIZATION REPORT");
        sb.AppendLine("═══════════════════════════════════════════════════════════════════");
        sb.AppendLine();
        
        // Executive Summary
        sb.AppendLine("EXECUTIVE SUMMARY");
        sb.AppendLine("─────────────────────────────────────────────────────────────────");
        sb.AppendLine($"Total Workers Required: {result.TotalWorkersNeeded} / {population} available");
        sb.AppendLine($"Population Utilization: {((double)result.TotalWorkersNeeded / population * 100):F1}%");
        sb.AppendLine($"Overall Efficiency: {(result.OverallEfficiency * 100):F1}%");
        sb.AppendLine($"Number of Building Types: {result.BuildingAllocations.Count}");
        sb.AppendLine($"Total Buildings Needed: {result.BuildingAllocations.Sum(a => a.BuildingCount)}");
        sb.AppendLine();
        
        // Warnings
        if (result.Warnings.Count > 0)
        {
            sb.AppendLine("⚠ WARNINGS");
            sb.AppendLine("─────────────────────────────────────────────────────────────────");
            foreach (var warning in result.Warnings)
            {
                sb.AppendLine($"  • {warning}");
            }
            sb.AppendLine();
        }
        
        // Building Allocations
        sb.AppendLine("BUILDING ALLOCATIONS");
        sb.AppendLine("─────────────────────────────────────────────────────────────────");
        
        foreach (var allocation in result.BuildingAllocations.OrderByDescending(a => a.TotalWorkers))
        {
            sb.AppendLine($"\n{allocation.BuildingName}");
            sb.AppendLine($"  Recipe: {allocation.RecipeName}");
            sb.AppendLine($"  Buildings: {allocation.BuildingCount}");
            sb.AppendLine($"  Workers per Building: {allocation.WorkersPerBuilding:F1}");
            sb.AppendLine($"  Total Workers: {allocation.TotalWorkers}");
            sb.AppendLine($"  Efficiency: {(allocation.Efficiency * 100):F1}%");
            
            if (allocation.ExpectedProduction.Count > 0)
            {
                sb.AppendLine($"  Expected Production (annual):");
                foreach (var prod in allocation.ExpectedProduction)
                {
                    sb.AppendLine($"    → {prod.Key}: {prod.Value:F0} units/year");
                }
            }
            
            if (allocation.ExpectedConsumption.Count > 0)
            {
                sb.AppendLine($"  Expected Consumption (annual):");
                foreach (var cons in allocation.ExpectedConsumption)
                {
                    sb.AppendLine($"    ← {cons.Key}: {cons.Value:F0} units/year");
                }
            }
        }
        
        sb.AppendLine();
        
        // Resource Balance
        sb.AppendLine("RESOURCE BALANCE (Annual Projection)");
        sb.AppendLine("─────────────────────────────────────────────────────────────────");
        sb.AppendLine("Resource                      Production    Consumption    Balance");
        sb.AppendLine("─────────────────────────────────────────────────────────────────");
        
        var finalProducts = targetProducts.Where(p => p.IsFinalProduct).Select(p => p.Name).ToHashSet();
        
        foreach (var balance in result.ResourceBalance.OrderByDescending(b => Math.Abs(b.Value)))
        {
            var isFinal = finalProducts.Contains(balance.Key) ? "★" : " ";
            var balanceStr = balance.Value >= 0 ? $"+{balance.Value:F0}" : $"{balance.Value:F0}";
            var status = balance.Value > 0 ? "SURPLUS" : balance.Value < -10 ? "DEFICIT" : "BALANCED";
            
            sb.AppendLine($"{isFinal} {balance.Key,-25} {status,12}  {balanceStr,10} units/year");
        }
        
        sb.AppendLine();
        sb.AppendLine("★ = Final Product Target");
        sb.AppendLine();
        
        // Action Items
        sb.AppendLine("ACTION ITEMS");
        sb.AppendLine("─────────────────────────────────────────────────────────────────");
        
        var buildingSummary = result.BuildingAllocations
            .GroupBy(a => a.BuildingName)
            .Select(g => new 
            { 
                Building = g.Key, 
                Count = g.Sum(a => a.BuildingCount),
                Workers = g.Sum(a => a.TotalWorkers)
            })
            .OrderBy(x => x.Building);
        
        foreach (var item in buildingSummary)
        {
            sb.AppendLine($"  → Build/Activate {item.Count} {item.Building}(s) with {item.Workers} total workers");
        }
        
        sb.AppendLine();
        sb.AppendLine("═══════════════════════════════════════════════════════════════════");
        
        return sb.ToString();
    }

    public string GenerateQuickSummary(OptimizationResult result, int population)
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"Workers: {result.TotalWorkersNeeded}/{population} ({((double)result.TotalWorkersNeeded / population * 100):F1}%)");
        sb.AppendLine($"Buildings: {result.BuildingAllocations.Sum(a => a.BuildingCount)} total");
        sb.AppendLine($"Efficiency: {(result.OverallEfficiency * 100):F1}%");
        
        if (result.Warnings.Count > 0)
            sb.AppendLine($"⚠ {result.Warnings.Count} warning(s)");
        
        return sb.ToString();
    }
}
