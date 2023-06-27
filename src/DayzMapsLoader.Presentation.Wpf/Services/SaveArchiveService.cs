using DayzMapsLoader.Presentation.Wpf.Contracts.Services;
using DayzMapsLoader.Presentation.Wpf.Models;

namespace DayzMapsLoader.Presentation.Wpf.Services
{
    public class SaveArchiveService : ISaveArchiveService
    {
        private readonly ISystemService _systemService;
        public SaveArchiveService(ISystemService systemService)
        {
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

        public string GetPathToSave()
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