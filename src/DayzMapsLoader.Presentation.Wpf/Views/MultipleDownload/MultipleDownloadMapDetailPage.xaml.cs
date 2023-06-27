using System.Windows.Controls;
using DayzMapsLoader.Presentation.Wpf.ViewModels.MultipleDownload;
using DayzMapsLoader.Presentation.Wpf.ViewModels.SingleDownload;

namespace DayzMapsLoader.Presentation.Wpf.Views.MultipleDownload;

public partial class MultipleDownloadMapDetailPage : Page
{
    public MultipleDownloadMapDetailPage(MultipleDownloadMapDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}