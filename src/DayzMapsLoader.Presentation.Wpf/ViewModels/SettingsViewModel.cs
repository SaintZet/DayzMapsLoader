using System.IO;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Contracts.ViewModels;
using DayzMapsLoader.Presentation.Wpf.Models;

using Microsoft.Extensions.Options;

namespace DayzMapsLoader.Presentation.Wpf.ViewModels;

public class SettingsViewModel : ObservableObject, INavigationAware
{
    private readonly AppConfig _appConfig;
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IDownloadArchiveService _downloadArchiveService;
    private readonly ISystemService _systemService;
    private readonly IApplicationInfoService _applicationInfoService;
    
    private AppTheme _theme;
    private ArchiveSaveOptions _saveOption;
    
    private string _defaultPathToSave;
    private string _versionDescription;
    
    private ICommand _setThemeCommand;
    private ICommand _setSaveOptionCommand;
    private ICommand _setDefaultPathToSave;
    private ICommand _privacyStatementCommand;

    public AppTheme Theme
    {
        get { return _theme; }
        set { SetProperty(ref _theme, value); }
    }
    public ArchiveSaveOptions SaveOption
    {
        get { return _saveOption; }
        set { SetProperty(ref _saveOption, value); }
    }
    public string DefaultPathToSave
    {
        get { return _defaultPathToSave; }
        set { SetProperty(ref _defaultPathToSave, value);  }
    }

    public string VersionDescription
    {
        get { return _versionDescription; }
        set { SetProperty(ref _versionDescription, value); }
    }

    public ICommand SetThemeCommand => _setThemeCommand ??= new RelayCommand<string>(OnSetTheme);
    public ICommand SetSaveOptionCommand => _setSaveOptionCommand ??= new RelayCommand<string>(OnSetSaveOption);
    public ICommand SetDefaultPathToSave => _setDefaultPathToSave ??= new RelayCommand(OnSetDefaultPathToSave);
    public ICommand PrivacyStatementCommand => _privacyStatementCommand ??= new RelayCommand(OnPrivacyStatement);

    public SettingsViewModel(
        IOptions<AppConfig> appConfig, 
        IThemeSelectorService themeSelectorService,
        IDownloadArchiveService downloadArchiveService,
        ISystemService systemService, 
        IApplicationInfoService applicationInfoService)
    {
        _appConfig = appConfig.Value;
        _themeSelectorService = themeSelectorService;
        _downloadArchiveService = downloadArchiveService;
        _systemService = systemService;
        _applicationInfoService = applicationInfoService;
    }

    public void OnNavigatedTo(object parameter)
    {
        VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
        Theme = _themeSelectorService.GetCurrentTheme();
        SaveOption = _downloadArchiveService.GetCurrentSaveOption();
        DefaultPathToSave = _downloadArchiveService.GetDefaultPathToSave();
    }

    public void OnNavigatedFrom()
    {
    }

    private void OnSetTheme(string themeName)
    {
        var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
        _themeSelectorService.SetTheme(theme);
    }
    
    private void OnSetSaveOption(string saveOption)
    {
        var option = (ArchiveSaveOptions)Enum.Parse(typeof(ArchiveSaveOptions), saveOption);
        _downloadArchiveService.SetSaveOption(option);
    }

    private void OnSetDefaultPathToSave()
    {
        var path = _systemService.OpenFolderDialog(DefaultPathToSave);
        DefaultPathToSave = path;
        _downloadArchiveService.SetDefaultPathToSave(path);
    }
    
    private void OnPrivacyStatement()
        => _systemService.OpenInWebBrowser(_appConfig.PrivacyStatement);
}
