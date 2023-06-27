using System.Windows.Controls;
using DayzMapsLoader.Presentation.Wpf.ViewModels;
using DayzMapsLoader.Presentation.Wpf.ViewModels.SingleDownload;

namespace DayzMapsLoader.Presentation.Wpf.Views.SingleDownload;

public partial class SingleDownloadProvidersPage : Page
{
    public SingleDownloadProvidersPage(SingleDownloadProvidersViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}