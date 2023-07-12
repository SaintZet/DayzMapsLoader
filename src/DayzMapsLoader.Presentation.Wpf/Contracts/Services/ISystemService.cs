namespace DayzMapsLoader.Presentation.Wpf.Contracts.Services;

public interface ISystemService
{
    void OpenInWebBrowser(string url);
    string OpenFolderDialog(string initialDirectory);
    void ShowErrorDialog(string message);
    void ShowInfoDialog(string message);
    void HasWriteAccessAsync(string directoryPath);
}
