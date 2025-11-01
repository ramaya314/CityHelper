using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;
using SettlementSurvivalOptimizer.UI.Services;

namespace SettlementSurvivalOptimizer.UI.ViewModels;

public class OptimizeViewModel : ViewModelBase
{
    private readonly DataService _dataService;
    private readonly OptimizationService _optimizationService;
    private int _population = 200;
    private string _resultsText = "Click 'Run Optimization' to see results";
    private bool _isOptimizing;

    public OptimizeViewModel(DataService dataService, OptimizationService optimizationService)
    {
        _dataService = dataService;
        _optimizationService = optimizationService;

        OptimizeCommand = ReactiveCommand.CreateFromTask(RunOptimizationAsync, outputScheduler: RxApp.MainThreadScheduler);
        
        _ = LoadPopulationAsync();
    }

    public int Population
    {
        get => _population;
        set
        {
            this.RaiseAndSetIfChanged(ref _population, value);
            _ = SavePopulationAsync(value);
        }
    }

    public string ResultsText
    {
        get => _resultsText;
        set => this.RaiseAndSetIfChanged(ref _resultsText, value);
    }

    public bool IsOptimizing
    {
        get => _isOptimizing;
        set => this.RaiseAndSetIfChanged(ref _isOptimizing, value);
    }

    public ReactiveCommand<Unit, Unit> OptimizeCommand { get; }

    private async Task LoadPopulationAsync()
    {
        var popStr = await _dataService.GetSettingAsync("Population");
        if (int.TryParse(popStr, out int pop))
        {
            _population = pop;
            this.RaisePropertyChanged(nameof(Population));
        }
    }

    private async Task SavePopulationAsync(int value)
    {
        await _dataService.SaveSettingAsync("Population", value.ToString());
    }

    private async Task RunOptimizationAsync()
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            IsOptimizing = true;
            ResultsText = "Optimizing...";
        });

        try
        {
            var buildings = await _dataService.GetAllBuildingsAsync();
            var resources = await _dataService.GetAllResourcesAsync();

            var result = _optimizationService.Optimize(buildings, resources, Population);

            // Generate report text
            var report = new System.Text.StringBuilder();
            report.AppendLine("═══════════════════════════════════════════════════════");
            report.AppendLine("     OPTIMIZATION RESULTS");
            report.AppendLine("═══════════════════════════════════════════════════════");
            report.AppendLine();
            report.AppendLine($"Total Workers Required: {result.TotalWorkersNeeded} / {result.TotalPopulation}");
            report.AppendLine($"Population Utilization: {(result.TotalWorkersNeeded / (double)result.TotalPopulation * 100):F1}%");
            report.AppendLine($"Overall Efficiency: {(result.OverallEfficiency * 100):F1}%");
            report.AppendLine($"Building Types: {result.BuildingAllocations.Count}");
            report.AppendLine($"Total Buildings: {result.BuildingAllocations.Sum(a => a.BuildingCount)}");
            report.AppendLine();

            if (result.Warnings.Any())
            {
                report.AppendLine("⚠ WARNINGS:");
                report.AppendLine("─────────────────────────────────────────────────────");
                foreach (var warning in result.Warnings)
                {
                    report.AppendLine($"  • {warning}");
                }
                report.AppendLine();
            }

            report.AppendLine("BUILDING ALLOCATIONS:");
            report.AppendLine("─────────────────────────────────────────────────────");
            foreach (var allocation in result.BuildingAllocations.OrderByDescending(a => a.TotalWorkers))
            {
                report.AppendLine();
                report.AppendLine($"{allocation.BuildingName}");
                report.AppendLine($"  Recipe: {allocation.RecipeName}");
                report.AppendLine($"  Buildings: {allocation.BuildingCount}");
                report.AppendLine($"  Workers/Building: {allocation.WorkersPerBuilding:F1}");
                report.AppendLine($"  Total Workers: {allocation.TotalWorkers}");
                report.AppendLine($"  Efficiency: {(allocation.Efficiency * 100):F1}%");
                
                if (allocation.ExpectedProduction.Any())
                {
                    report.AppendLine($"  Production:");
                    foreach (var prod in allocation.ExpectedProduction)
                    {
                        report.AppendLine($"    → {prod.Key}: {prod.Value:F0}/year");
                    }
                }
            }

            report.AppendLine();
            report.AppendLine("RESOURCE BALANCE:");
            report.AppendLine("─────────────────────────────────────────────────────");
            foreach (var balance in result.ResourceBalance.OrderByDescending(b => Math.Abs(b.Value)))
            {
                var status = balance.Value > 0 ? "SURPLUS" : balance.Value < -10 ? "DEFICIT" : "BALANCED";
                var sign = balance.Value >= 0 ? "+" : "";
                report.AppendLine($"{balance.Key,-30} {status,10} {sign}{balance.Value:F0}/year");
            }

            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                ResultsText = report.ToString();
            });
        }
        catch (Exception ex)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                ResultsText = $"Error during optimization:\n{ex.Message}\n\n{ex.StackTrace}";
            });
        }
        finally
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsOptimizing = false;
            });
        }
    }
}
