using System.Windows.Controls;

using DayzMapsLoader.Presentation.Wpf.ViewModels;

namespace DayzMapsLoader.Presentation.Wpf.Views;

public partial class ContentGridMapsPage : Page
{
    public ContentGridMapsPage(ContentGridMapsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}