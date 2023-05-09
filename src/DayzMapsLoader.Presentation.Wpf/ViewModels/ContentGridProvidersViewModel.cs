using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DayzMapsLoader.Application.Abstractions.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels;

public class ContentGridProvidersViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IMapProvidersRepository _mapProvidersRepository;

    private ICommand _navigateToDetailCommand;

    public ContentGridProvidersViewModel(IMapProvidersRepository mapProvidersRepository, INavigationService navigationService)
    {
        _mapProvidersRepository = mapProvidersRepository;
        _navigationService = navigationService;
    }

    public ICommand NavigateToDetailCommand => _navigateToDetailCommand ??= new RelayCommand<MapProvider>(NavigateToDetail);

    public ObservableCollection<MapProvider> Source { get; } = new ObservableCollection<MapProvider>();

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        var providers = await _mapProvidersRepository.GetAllMapProvidersAsync();

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
        _navigationService.NavigateTo(typeof(ContentGridMapsViewModel).FullName, provider.Id);
    }
}