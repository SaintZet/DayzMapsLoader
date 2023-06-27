using DayzMapsLoader.Presentation.Wpf.Models;
using MediatR;

namespace DayzMapsLoader.Presentation.Wpf.Contracts.Services
{
    public interface IDownloadArchiveService
    {
        void SetSaveOption(ArchiveSaveOptions option);
        
        ArchiveSaveOptions GetCurrentSaveOption();
        
        void SetDefaultPathToSave(string path);

        string GetDefaultPathToSave();

        Task DownloadArchive(IRequest<(byte[] data, string name)> request);
    }
}