using System.Windows.Controls;
using DayzMapsLoader.Presentation.Wpf.ViewModels.MultipleDownload;

namespace DayzMapsLoader.Presentation.Wpf.Views.MultipleDownload;

public partial class MultipleDownloadProvidersPage : Page
{
    public MultipleDownloadProvidersPage(MultipleDownloadProvidersViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}