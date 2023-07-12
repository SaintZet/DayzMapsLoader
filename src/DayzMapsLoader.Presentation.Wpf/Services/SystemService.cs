using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;

using DayzMapsLoader.Presentation.Wpf.Contracts.Services;

namespace DayzMapsLoader.Presentation.Wpf.Services;

public class SystemService : ISystemService
{
    public void OpenInWebBrowser(string url)
    {
        var psi = new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        };
        Process.Start(psi);
    }

    public string OpenFolderDialog(string initialDirectory)
    {
        var folderPath = string.Empty;

        using var dialog = new FolderBrowserDialog();
        dialog.SelectedPath = initialDirectory;

        var result = dialog.ShowDialog();

        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            folderPath = dialog.SelectedPath;

        return folderPath;
    }

    public void ShowErrorDialog(string message) => 
        System.Windows.MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

    public void ShowInfoDialog(string message)=> 
        System.Windows.MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
    public async void HasWriteAccessAsync(string directoryPath)
    {
        var tempFilePath = Path.Combine(directoryPath, Guid.NewGuid().ToString("N"));
        try
        {
            var random = new Random();
            var bytes = new byte[1024];
            random.NextBytes(bytes);
            
            await File.WriteAllBytesAsync(tempFilePath, bytes);
        }
        finally
        {
            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);
        }
    }
}
