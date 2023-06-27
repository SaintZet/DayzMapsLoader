using System.IO;
using DayzMapsLoader.Domain.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Models;
using MediatR;

namespace DayzMapsLoader.Presentation.Wpf.Services
{
    public class DownloadArchiveService : IDownloadArchiveService
    {
        private readonly IMediator _mediator;
        private readonly ISystemService _systemService;
        
        public DownloadArchiveService(IMediator mediator, ISystemService systemService)
        {
            _mediator = mediator;
            _systemService = systemService;
        }
        
        public void SetSaveOption(ArchiveSaveOptions option)
        {
            App.Current.Properties["SaveOption"] = option.ToString();
        }

        public ArchiveSaveOptions GetCurrentSaveOption()
        {
            if (!App.Current.Properties.Contains("SaveOption"))
                return ArchiveSaveOptions.Always;

            var saveOption = App.Current.Properties["SaveOption"].ToString();
            Enum.TryParse(saveOption, out ArchiveSaveOptions option);
            return option;
        }

        public void SetDefaultPathToSave(string path)
        {
            App.Current.Properties["DefaultPathToSave"] = path;
        }

        public string GetDefaultPathToSave()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\";

            if (App.Current.Properties.Contains("DefaultPathToSave"))
                path = App.Current.Properties["DefaultPathToSave"].ToString();

            return path;
        }
        
        public async Task DownloadArchive(IRequest<(byte[] data, string name)> request)
        {
            var filePath = GetPathToSave();
            try
            {
                _systemService.HasWriteAccessAsync(filePath);
            
                var (file, fileName) = await _mediator.Send(request);
            
                await File.WriteAllBytesAsync(Path.Combine(filePath, fileName), file);
            }
            catch (UnauthorizedAccessException ex)
            {
                _systemService.ShowErrorDialog("Access error: " + ex.Message);
            }
            catch (IOException ex)
            {
                _systemService.ShowErrorDialog("I/O error: " + ex.Message);
            }
            catch (Exception ex)
            {
                _systemService.ShowErrorDialog("Core error: " + ex.Message);
            }
        } 
        
        private string GetPathToSave()
        {
            var defaultPath = GetDefaultPathToSave();

            if (GetCurrentSaveOption() == ArchiveSaveOptions.ToDefaultPath)
                return defaultPath;

            var newPath= _systemService.OpenFolderDialog(defaultPath);
            SetDefaultPathToSave(newPath);
            
            return newPath;
        }
    }
}