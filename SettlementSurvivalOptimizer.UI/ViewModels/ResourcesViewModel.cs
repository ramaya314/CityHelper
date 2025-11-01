using System.Threading.Tasks;
using Avalonia.Threading;
using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;
using SettlementSurvivalOptimizer.UI.Services;
using SettlementSurvivalOptimizer.UI.Models;

namespace SettlementSurvivalOptimizer.UI.ViewModels;

public class ResourcesViewModel : ViewModelBase
{
    private readonly DataService _dataService;
    private ResourceEntity? _selectedResource;
    private bool _isEditing;

    public ResourcesViewModel(DataService dataService)
    {
        _dataService = dataService;
        Resources = new ObservableCollection<ResourceEntity>();

        AddCommand = ReactiveCommand.Create(AddResource, outputScheduler: RxApp.MainThreadScheduler);
        EditCommand = ReactiveCommand.Create(EditResource, outputScheduler: RxApp.MainThreadScheduler);
        DeleteCommand = ReactiveCommand.Create(DeleteResource, outputScheduler: RxApp.MainThreadScheduler);
        SaveCommand = ReactiveCommand.CreateFromTask(SaveResourceAsync, outputScheduler: RxApp.MainThreadScheduler);
        CancelCommand = ReactiveCommand.Create(CancelEdit, outputScheduler: RxApp.MainThreadScheduler);

        _ = LoadDataAsync();
    }

    public ObservableCollection<ResourceEntity> Resources { get; }

    public ResourceEntity? SelectedResource
    {
        get => _selectedResource;
        set => this.RaiseAndSetIfChanged(ref _selectedResource, value);
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
        var resources = await _dataService.GetAllResourcesAsync();
        
        System.Diagnostics.Debug.WriteLine($"[ResourcesViewModel] Loaded {resources.Count} resources from database");
        
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Resources.Clear();
            foreach (var resource in resources)
            {
                System.Diagnostics.Debug.WriteLine($"[ResourcesViewModel] Adding resource: {resource.Name}, Stock: {resource.Stock}");
                Resources.Add(resource);
            }
            System.Diagnostics.Debug.WriteLine($"[ResourcesViewModel] Final Resources.Count = {Resources.Count}");
            
            // Force property change notification
            this.RaisePropertyChanged(nameof(Resources));
        });
    }

    private void AddResource()
    {
        SelectedResource = new ResourceEntity();
        IsEditing = true;
    }

    private void EditResource()
    {
        if (SelectedResource != null)
        {
            IsEditing = true;
        }
    }

    private async void DeleteResource()
    {
        if (SelectedResource != null)
        {
            await _dataService.DeleteResourceAsync(SelectedResource.Id);
            await LoadDataAsync();
            
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                SelectedResource = null;
            });
        }
    }

    private async Task SaveResourceAsync()
    {
        if (SelectedResource != null)
        {
            await _dataService.SaveResourceAsync(SelectedResource);
            await LoadDataAsync();
            
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                IsEditing = false;
                SelectedResource = null;
            });
        }
    }

    private void CancelEdit()
    {
        IsEditing = false;
        SelectedResource = null;
        _ = LoadDataAsync();
    }
}
