using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DayzMapsLoader.Application.Abstractions.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels;

public class ContentGridMapsViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IProvidedMapsRepository _sampleDataService;
    private ICommand _navigateToDetailCommand;
    private int _selectedProviderId;

    public ContentGridMapsViewModel(IProvidedMapsRepository sampleDataService, INavigationService navigationService)
    {
        _sampleDataService = sampleDataService;
        _navigationService = navigationService;
    }

    public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<ProvidedMap>(NavigateToDetail);

    public ObservableCollection<ProvidedMap> Source { get; } = new ObservableCollection<ProvidedMap>();

    public async void OnNavigatedTo(object parameter)
    {
        if (parameter is not null)
            _selectedProviderId = (int)parameter;

        Source.Clear();

        var providedMaps = await _sampleDataService.GetAllProvidedMapsByProviderIdAsync(_selectedProviderId);

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
        _navigationService.NavigateTo(typeof(ContentGridMapDetailViewModel).FullName, map);
    }
}