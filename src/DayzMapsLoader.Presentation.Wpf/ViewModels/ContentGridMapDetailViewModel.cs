using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels;

public class ContentGridMapDetailViewModel : ObservableObject, INavigationAware
{
    private readonly IProvidedMapsRepository _sampleDataService;
    private ProvidedMap _item;

    public ContentGridMapDetailViewModel(IProvidedMapsRepository sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public ProvidedMap Item
    {
        get { return _item; }
        set
        {
            SetProperty(ref _item, value);
            SetZoomLevel(value);
        }
    }

    public ObservableCollection<MapType> MapTypes { get; set; } = new();

    public MapType SelectedMapType { get; set; } = new();

    public ObservableCollection<int> ZoomLevels { get; set; } = new();

    public int SelectedZoomLevel { get; set; }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is not ProvidedMap providedMap)
            throw new ArgumentException("parameter is not correct!");

        MapTypes.Clear();
        ZoomLevels.Clear();

        Item = providedMap;

        var providedMapsByProviderId = await _sampleDataService.GetAllProvidedMapsByProviderIdAsync(providedMap.MapProvider.Id);

        providedMapsByProviderId
            .Where(x => x.Map.Id == providedMap.Map.Id)
            .Select(i => i.MapType)
            .ToList()
            .ForEach(item => MapTypes.Add(item));

        SelectedMapType = MapTypes[0];
        OnPropertyChanged(nameof(SelectedMapType));
    }

    public void OnNavigatedFrom()
    {
    }

    private void SetZoomLevel(ProvidedMap value)
    {
        for (int i = 0; i < value.MaxMapLevel; i++)
            ZoomLevels.Add(i);
    }
}