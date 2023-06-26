using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DayzMapsLoader.Core.Features.MapArchive.Queries;
using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;
using MediatR;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels.MultipleDownload;

public class MultipleDownloadMapDetailViewModel : ObservableObject, INavigationAware
{
    private readonly IMediator _mediator;

    public MultipleDownloadMapDetailViewModel(IMediator mediator)
    {
        _mediator = mediator;
        DownloadMapsCommand = new RelayCommand(DownloadMaps);
    }

    public MapProvider Provider { get; set; }

    public ICommand DownloadMapsCommand { get; }

    public ObservableCollection<int> ZoomLevels { get; set; }

    public int SelectedZoomLevel { get; set; }

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is not MapProvider provider)
            throw new ArgumentException("parameter is not correct!");

        Provider = provider;
        
        var query = new GetProvidedMapsByProviderIdQuery(provider.Id);
        var providedMapsByProviderId = await _mediator.Send(query);
        
        var mapsByProviderId = providedMapsByProviderId.ToList();
        
        var maxCommonLevel = mapsByProviderId.Select(obj => obj.MaxMapLevel).Min();
        maxCommonLevel = mapsByProviderId.All(obj => obj.MaxMapLevel >= maxCommonLevel) ? maxCommonLevel : 0; 
        
        ZoomLevels = GetZoomLevelsObservableCollection(maxCommonLevel);
        OnPropertyChanged(nameof(ZoomLevels));
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

    private async void DownloadMaps()
    {
        var query = new GetAllMapsImagesArchiveQuery(Provider.Id, SelectedZoomLevel);
        var (data, name) = await _mediator.Send(query);

        var filePath = Path.Combine("C:\\Users\\s.chepets\\RiderProjects\\Test output", name);
        await File.WriteAllBytesAsync(filePath, data);
    }
}