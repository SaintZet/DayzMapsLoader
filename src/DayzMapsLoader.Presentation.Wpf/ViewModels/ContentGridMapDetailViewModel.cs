using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DayzMapsLoader.Core.Features.MapArchive.Queries;
using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;

using MediatR;

using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels;

public class ContentGridMapDetailViewModel : ObservableObject, INavigationAware
{
    private readonly IMediator _mediator;

    public ContentGridMapDetailViewModel(IMediator mediator)
    {
        _mediator = mediator;
        DownloadMapCommand = new RelayCommand(DownloadMap);
    }

    public ProvidedMap Map { get; set; }

    public ICommand DownloadMapCommand { get; }

    public ObservableCollection<MapType> MapTypes { get; set; }

    public MapType SelectedMapType { get; set; }

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
        for (int i = 0; i < maxValue; i++)
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
        var query = new GetMapImageArchiveQuery(Map.MapProvider.Id, Map.Map.Id, SelectedMapType.Id, SelectedZoomLevel);
        var (data, name) = await _mediator.Send(query);

        string filePath = Path.Combine("D:\\Projects\\Test", name);
        File.WriteAllBytes(filePath, data);
    }
}