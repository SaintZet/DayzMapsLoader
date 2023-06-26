using System.Windows.Controls;
using DayzMapsLoader.Presentation.Wpf.ViewModels.SingleDownload;

namespace DayzMapsLoader.Presentation.Wpf.Views.SingleDownload;

public partial class SingleDownloadMapDetailPage : Page
{
    public SingleDownloadMapDetailPage(SingleDownloadMapDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}