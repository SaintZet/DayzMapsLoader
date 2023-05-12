using System.Windows.Controls;

using DayzMapsLoader.Presentation.Wpf.ViewModels;

namespace DayzMapsLoader.Presentation.Wpf.Views;

public partial class ContentGridProvidersPage : Page
{
    public ContentGridProvidersPage(ContentGridProvidersViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}