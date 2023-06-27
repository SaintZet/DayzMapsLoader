using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;

using MediatR;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels.SingleDownload;

public class SingleDownloadMapsViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IMediator _mediator;

    private ICommand _navigateToDetailCommand;
    private int _selectedProviderId;

    public SingleDownloadMapsViewModel(IMediator mediator, INavigationService navigationService)
    {
        _mediator = mediator;
        _navigationService = navigationService;
    }

    public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<ProvidedMap>(NavigateToDetail);

    public ObservableCollection<ProvidedMap> Source { get; } = new();

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is not null)
            _selectedProviderId = (int)parameter;

        Source.Clear();

        var query = new GetProvidedMapsByProviderIdQuery(_selectedProviderId);
        var providedMaps = await _mediator.Send(query);

        providedMaps.GroupBy(x => x.Map.Name)
                             .Select(group => group.First())
                             .ToList()
                             .ForEach(providedMap => Source.Add(providedMap));
    }

    public void OnNavigatedFrom()
    {
    }

    private void NavigateToDetail(ProvidedMap map)
    {
        _navigationService.NavigateTo(typeof(SingleDownloadMapDetailViewModel).FullName, map);
    }
}