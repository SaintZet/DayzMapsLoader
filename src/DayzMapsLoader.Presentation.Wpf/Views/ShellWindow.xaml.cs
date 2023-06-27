using System.Windows.Controls;

using DayzMapsLoader.Presentation.Wpf.Contracts.Views;
using DayzMapsLoader.Presentation.Wpf.ViewModels;

using MahApps.Metro.Controls;

namespace DayzMapsLoader.Presentation.Wpf.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => ShellFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
