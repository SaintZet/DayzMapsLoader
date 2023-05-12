using System.Windows.Controls;

using DayzMapsLoader.Presentation.Wpf.ViewModels;

namespace DayzMapsLoader.Presentation.Wpf.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
