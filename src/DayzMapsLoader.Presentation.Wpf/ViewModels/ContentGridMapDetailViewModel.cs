using CommunityToolkit.Mvvm.ComponentModel;

using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;

using MediatR;

using System.Collections.ObjectModel;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels;

public class ContentGridMapDetailViewModel : ObservableObject, INavigationAware
{
    private readonly IMediator _mediator;
    private ProvidedMap _item;

    public ContentGridMapDetailViewModel(IMediator mediator)
    {
        _mediator = mediator;
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

        var query = new GetProvidedMapsByProviderIdQuery(providedMap.MapProvider.Id);
        var providedMapsByProviderId = await _mediator.Send(query);

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