using System.Collections.ObjectModel;
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
    private readonly IDownloadArchiveService _downloadArchiveService;
    private readonly ISystemService _systemService;
    
    private ICommand _downloadMapCommand;
    private ICommand _linkCommand;
    
    private ProvidedMap _map;
    private MapType _selectedMapType;
    private ObservableCollection<MapType> _mapTypes;
    private ObservableCollection<int> _zoomLevels;
    private bool _isBusy;
    
    public SingleDownloadMapDetailViewModel(IMediator mediator, IDownloadArchiveService downloadArchiveService, ISystemService systemService)
    {
        _mediator = mediator;
        _downloadArchiveService = downloadArchiveService;
        _systemService = systemService;
    }

    public ICommand DownloadMapCommand => _downloadMapCommand ??= new RelayCommand(DownloadMap);
    public ICommand LinkCommand => _linkCommand ??= new RelayCommand(OnLinkCommand);
    
    public ProvidedMap Map
    {
        get => _map;
        set => SetProperty(ref _map, value);
    }

    public MapType SelectedMapType 
    {
        get => _selectedMapType;
        set => SetProperty(ref _selectedMapType, value);
    }
    
    public ObservableCollection<MapType> MapTypes
    {
        get => _mapTypes;
        set => SetProperty(ref _mapTypes, value);
    }

    public ObservableCollection<int> ZoomLevels
    {
        get => _zoomLevels;
        set => SetProperty(ref _zoomLevels, value);
    }
    
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public int SelectedZoomLevel { get; set; }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is not ProvidedMap providedMap)
            throw new ArgumentException("parameter is not correct!");

        Map = providedMap;
        MapTypes = await GetMapTypesAsync(providedMap.MapProvider.Id, providedMap.Map.Id);
        SelectedMapType = MapTypes[0];
        ZoomLevels = GetZoomLevelsObservableCollection(Map.MaxMapLevel);
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
        IsBusy = true;
        try
        {
            var query = new GetMapImageArchiveQuery(Map.MapProvider.Id, Map.Map.Id, SelectedMapType.Id, SelectedZoomLevel);
            await _downloadArchiveService.DownloadArchive(query);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    private void OnLinkCommand()
        => _systemService.OpenInWebBrowser(_map.Map.Link);
}