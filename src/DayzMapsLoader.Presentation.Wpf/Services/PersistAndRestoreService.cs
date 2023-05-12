using System.Collections;
using System.IO;
using System.Windows;

using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Models;

using Microsoft.Extensions.Options;

namespace DayzMapsLoader.Presentation.Wpf.Services;

public class PersistAndRestoreService : IPersistAndRestoreService
{
    private readonly IFileService _fileService;
    private readonly AppConfig _appConfig;
    private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public PersistAndRestoreService(IFileService fileService, IOptions<AppConfig> appConfig)
    {
        _fileService = fileService;
        _appConfig = appConfig.Value;
    }

    public void PersistData()
    {
        if (Application.Current.Properties != null)
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.AppPropertiesFileName;
            _fileService.Save(folderPath, fileName, Application.Current.Properties);
        }
    }

    public void RestoreData()
    {
        var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
        var fileName = _appConfig.AppPropertiesFileName;
        var properties = _fileService.Read<IDictionary>(folderPath, fileName);
        if (properties != null)
        {
            foreach (DictionaryEntry property in properties)
            {
                Application.Current.Properties.Add(property.Key, property.Value);
            }
        }
    }
}