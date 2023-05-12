using DayzMapsLoader.Presentation.Wpf.Models;

namespace DayzMapsLoader.Presentation.Wpf.Contracts.Services;

public interface IThemeSelectorService
{
    void InitializeTheme();

    void SetTheme(AppTheme theme);

    AppTheme GetCurrentTheme();
}
