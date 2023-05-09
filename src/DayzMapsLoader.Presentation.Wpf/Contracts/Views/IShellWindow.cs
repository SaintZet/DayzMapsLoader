using System.Windows.Controls;

namespace DayzMapsLoader.Presentation.Wpf.Contracts.Views;

public interface IShellWindow
{
    Frame GetNavigationFrame();

    void ShowWindow();

    void CloseWindow();
}
