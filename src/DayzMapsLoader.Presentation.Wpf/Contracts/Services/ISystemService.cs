namespace DayzMapsLoader.Presentation.Wpf.Contracts.Services;

public interface ISystemService
{
    void OpenInWebBrowser(string url);
    string OpenFolderDialog(string initialDirectory);
    void ShowErrorDialog(string errorMessage);
    void HasWriteAccessAsync(string directoryPath);
}
