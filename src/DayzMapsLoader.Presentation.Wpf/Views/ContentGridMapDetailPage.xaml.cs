using System.Windows.Controls;

using DayzMapsLoader.Presentation.Wpf.ViewModels;

namespace DayzMapsLoader.Presentation.Wpf.Views;

public partial class ContentGridMapDetailPage : Page
{
    public ContentGridMapDetailPage(ContentGridMapDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}