using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DayzMapsLoader.Core.Features.MapArchive.Queries;
using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;

using MediatR;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels.SingleDownload;

public class SingleDownloadMapDetailViewModel : ObservableObject, INavigationAware
{
    private readonly IMediator _mediator;
    private readonly ISaveArchiveService _saveArchiveService;
    private readonly ISystemService _systemService;

    public SingleDownloadMapDetailViewModel(IMediator mediator, ISaveArchiveService saveArchiveService, ISystemService systemService)
    {
        _mediator = mediator;
        _saveArchiveService = saveArchiveService;
        _systemService = systemService;
        
        DownloadMapCommand = new RelayCommand(DownloadMap);
    }

    public ProvidedMap Map { get; set; }

    public ICommand DownloadMapCommand { get; }

    public ObservableCollection<MapType> MapTypes { get; set; }

    public MapType SelectedMapType { get; set; }
    
    private bool _isBusy;
    public bool IsBusy
    {
        get { return _isBusy; }
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<int> ZoomLevels { get; set; }

    public int SelectedZoomLevel { get; set; }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is not ProvidedMap providedMap)
            throw new ArgumentException("parameter is not correct!");

        Map = providedMap;
        OnPropertyChanged(nameof(MapTypes));

        ZoomLevels = GetZoomLevelsObservableCollection(Map.MaxMapLevel);
        OnPropertyChanged(nameof(ZoomLevels));

        MapTypes = await GetMapTypesAsync(providedMap.MapProvider.Id, providedMap.Map.Id);
        OnPropertyChanged(nameof(MapTypes));

        SelectedMapType = MapTypes[0];
        OnPropertyChanged(nameof(SelectedMapType));
    }

    public void OnNavigatedFrom()
    {
    }

    private static ObservableCollection<int> GetZoomLevelsObservableCollection(int maxValue)
    {
        var zoomLevels = new ObservableCollection<int>();
        for (var i = 0; i < maxValue; i++)
            zoomLevels.Add(i);

        return zoomLevels;
    }

    private async Task<ObservableCollection<MapType>> GetMapTypesAsync(int mapProviderId, int mapId)
    {
        var query = new GetProvidedMapsByProviderIdQuery(mapProviderId);
        var providedMapsByProviderId = await _mediator.Send(query);

        var result = new ObservableCollection<MapType>();
        providedMapsByProviderId
            .Where(x => x.Map.Id == mapId)
            .Select(i => i.MapType)
            .ToList()
            .ForEach(item => result.Add(item));

        return result;
    }

    private async void DownloadMap()
    {
        var filePath = _saveArchiveService.GetPathToSave();
        try
        {
            _systemService.HasWriteAccessAsync(filePath);
        }
        catch (Exception ex)
        {
            _systemService.ShowErrorDialog("Error: " + ex.Message);
            return;
        }
        
        var query = new GetMapImageArchiveQuery(Map.MapProvider.Id, Map.Map.Id, SelectedMapType.Id, SelectedZoomLevel);
        IsBusy = true;
        var (file, fileName) = await _mediator.Send(query);
        IsBusy = false;
        
        try
        {
            await File.WriteAllBytesAsync(Path.Combine(filePath, fileName), file);
        }
        catch (Exception ex)
        {
            _systemService.ShowErrorDialog("Error: " + ex.Message);
        }
    }
}