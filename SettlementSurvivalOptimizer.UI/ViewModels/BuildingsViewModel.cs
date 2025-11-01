using System.Threading.Tasks;
using Avalonia.Threading;
using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;
using SettlementSurvivalOptimizer.UI.Services;
using SettlementSurvivalOptimizer.UI.Models;

namespace SettlementSurvivalOptimizer.UI.ViewModels;

public class BuildingsViewModel : ViewModelBase
{
    private readonly DataService _dataService;
    private BuildingEntity? _selectedBuilding;
    private bool _isEditing;

    public BuildingsViewModel(DataService dataService)
    {
        _dataService = dataService;
        Buildings = new ObservableCollection<BuildingEntity>();

        AddCommand = ReactiveCommand.Create(AddBuilding, outputScheduler: RxApp.MainThreadScheduler);
        EditCommand = ReactiveCommand.Create(EditBuilding, outputScheduler: RxApp.MainThreadScheduler);
        DeleteCommand = ReactiveCommand.Create(DeleteBuilding, outputScheduler: RxApp.MainThreadScheduler);
        SaveCommand = ReactiveCommand.CreateFromTask(SaveBuildingAsync, outputScheduler: RxApp.MainThreadScheduler);
        CancelCommand = ReactiveCommand.Create(CancelEdit, outputScheduler: RxApp.MainThreadScheduler);

        _ = LoadDataAsync();
    }

    public ObservableCollection<BuildingEntity> Buildings { get; }

    public BuildingEntity? SelectedBuilding
    {
        get => _selectedBuilding;
        set => this.RaiseAndSetIfChanged(ref _selectedBuilding, value);
    }

    public bool IsEditing
    {
        get => _isEditing;
        set => this.RaiseAndSetIfChanged(ref _isEditing, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public async Task LoadDataAsync()
    {
        var buildings = await _dataService.GetAllBuildingsAsync();
        
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Buildings.Clear();
            foreach (var building in buildings)
            {
                Buildings.Add(building);
            }
        });
    }

    private void AddBuilding()
    {
        SelectedBuilding = new BuildingEntity
        {
            MaxWorkers = 4,
            PartialStaffingEfficiency = 0.9
        };
        IsEditing = true;
    }

    private void EditBuilding()
    {
        if (SelectedBuilding != null)
        {
            IsEditing = true;
        }
    }

    private async void DeleteBuilding()
    {
        if (SelectedBuilding != null)
        {
            await _dataService.DeleteBuildingAsync(SelectedBuilding.Id);
            await LoadDataAsync();
            
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                SelectedBuilding = null;
            });
        }
    }

    private async Task SaveBuildingAsync()
    {
        if (SelectedBuilding != null)
        {
            await _dataService.SaveBuildingAsync(SelectedBuilding);
            await LoadDataAsync();
            
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsEditing = false;
                SelectedBuilding = null;
            });
        }
    }

    private void CancelEdit()
    {
        IsEditing = false;
        SelectedBuilding = null;
        _ = LoadDataAsync();
    }
}
