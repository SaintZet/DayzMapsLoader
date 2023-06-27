using DayzMapsLoader.Presentation.Wpf.Models;

namespace DayzMapsLoader.Presentation.Wpf.Contracts.Services
{
    public interface ISaveArchiveService
    {
        void SetSaveOption(ArchiveSaveOptions option);
        
        ArchiveSaveOptions GetCurrentSaveOption();
        
        void SetDefaultPathToSave(string path);

        string GetDefaultPathToSave();

        string GetPathToSave();
    }
}