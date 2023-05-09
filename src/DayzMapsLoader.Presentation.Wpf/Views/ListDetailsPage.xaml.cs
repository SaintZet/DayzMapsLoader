using System.Windows.Controls;

using DayzMapsLoader.Presentation.Wpf.ViewModels;

namespace DayzMapsLoader.Presentation.Wpf.Views;

public partial class ListDetailsPage : Page
{
    public ListDetailsPage(ListDetailsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
