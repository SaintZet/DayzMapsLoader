using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DayzMapsLoader.Core.Features.MapProviders.Queries;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;

using MediatR;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels.MultipleDownload;

public class MultipleDownloadProvidersViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IMediator _mediator;

    private ICommand _navigateToDetailCommand;

    public MultipleDownloadProvidersViewModel(IMediator mediator, INavigationService navigationService)
    {
        _mediator = mediator;
        _navigationService = navigationService;
    }

    public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<MapProvider>(NavigateToDetail);

    public ObservableCollection<MapProvider> Source { get; } = new();

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        var query = new GetMapProvidersQuery();
        var providers = await _mediator.Send(query);

        foreach (var provider in providers)
        {
            Source.Add(provider);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    private void NavigateToDetail(MapProvider provider)
    {
        _navigationService.NavigateTo(typeof(MultipleDownloadMapDetailViewModel).FullName, provider);
    }
}