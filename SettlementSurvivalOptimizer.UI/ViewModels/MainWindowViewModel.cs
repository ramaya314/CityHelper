using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;
using SettlementSurvivalOptimizer.UI.Services;
using SettlementSurvivalOptimizer.UI.Models;

namespace SettlementSurvivalOptimizer.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly DataService _dataService;
    private readonly OptimizationService _optimizationService;

    private ViewModelBase _currentPage;
    private string _statusMessage = string.Empty;

    public MainWindowViewModel()
    {
        _dataService = new DataService();
        _optimizationService = new OptimizationService();
        
        // Initialize pages
        ResourcesViewModel = new ResourcesViewModel(_dataService);
        BuildingsViewModel = new BuildingsViewModel(_dataService);
        OptimizeViewModel = new OptimizeViewModel(_dataService, _optimizationService);
        
        _currentPage = ResourcesViewModel;

        // Commands
        ShowResourcesCommand = ReactiveCommand.Create(ShowResources, outputScheduler: RxApp.MainThreadScheduler);
        ShowBuildingsCommand = ReactiveCommand.Create(ShowBuildings, outputScheduler: RxApp.MainThreadScheduler);
        ShowOptimizeCommand = ReactiveCommand.Create(ShowOptimize, outputScheduler: RxApp.MainThreadScheduler);
        
        // Initialize with sample data if needed
        _ = InitializeDataAsync();
    }

    public ResourcesViewModel ResourcesViewModel { get; }
    public BuildingsViewModel BuildingsViewModel { get; }
    public OptimizeViewModel OptimizeViewModel { get; }

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set => this.RaiseAndSetIfChanged(ref _statusMessage, value);
    }

    public ReactiveCommand<Unit, Unit> ShowResourcesCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowBuildingsCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowOptimizeCommand { get; }

    private void ShowResources()
    {
        CurrentPage = ResourcesViewModel;
        _ = ResourcesViewModel.LoadDataAsync();
    }

    private void ShowBuildings()
    {
        CurrentPage = BuildingsViewModel;
        _ = BuildingsViewModel.LoadDataAsync();
    }

    private void ShowOptimize()
    {
        CurrentPage = OptimizeViewModel;
    }

    private async Task InitializeDataAsync()
    {
        await _dataService.SeedSampleDataAsync();
        StatusMessage = "Ready";
    }
}
