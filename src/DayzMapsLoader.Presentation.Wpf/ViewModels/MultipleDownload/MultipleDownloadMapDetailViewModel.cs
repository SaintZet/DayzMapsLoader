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

namespace DayzMapsLoader.Presentation.Wpf.ViewModels.MultipleDownload;

public class MultipleDownloadMapDetailViewModel : ObservableObject, INavigationAware
{
    private readonly IMediator _mediator;
    private readonly IDownloadArchiveService _downloadArchiveService;
    private readonly ISystemService _systemService;

    private ICommand _downloadMapsCommand;
    private ICommand _linkCommand;

    private MapProvider _provider;
    private ObservableCollection<int>  _zoomLevels;
    private int _selectedZoomLevel;
    private bool _isBusy;
    
    public MultipleDownloadMapDetailViewModel(IMediator mediator, IDownloadArchiveService downloadArchiveService, ISystemService systemService)
    {
        _mediator = mediator;
        _downloadArchiveService = downloadArchiveService;
        _systemService = systemService;
    }

    public ICommand DownloadMapsCommand => _downloadMapsCommand ??= new RelayCommand(DownloadMaps);
    public ICommand LinkCommand => _linkCommand ??= new RelayCommand(OnLinkCommand);
    public MapProvider Provider
    {
        get => _provider;
        set => SetProperty(ref _provider, value);
    }

    public ObservableCollection<int> ZoomLevels
    {
        get => _zoomLevels;
        set => SetProperty(ref _zoomLevels, value);
    }

    public int SelectedZoomLevel
    {
        get => _selectedZoomLevel;
        set => SetProperty(ref _selectedZoomLevel, value);
    }

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
    
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private async void DownloadMaps()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllMapsImagesArchiveQuery(Provider.Id, SelectedZoomLevel);
            await _downloadArchiveService.DownloadArchive(request);
        }
        finally
        {
            IsBusy = false;
        }
    }
    
    private void OnLinkCommand()
        => _systemService.OpenInWebBrowser(_provider.Link);
}