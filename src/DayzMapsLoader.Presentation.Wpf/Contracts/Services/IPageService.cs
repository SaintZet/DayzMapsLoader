using System.Windows.Controls;

namespace DayzMapsLoader.Presentation.Wpf.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
