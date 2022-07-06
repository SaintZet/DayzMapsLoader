namespace RequestsHub.Domain.Services
{
    internal class PathsService
    {
        public string generalFolderToSave;
        private readonly string folderToSave;
        private readonly string providerName;
        private readonly string folderType;
        private readonly string folderZoom;

        public PathsService(string folderToSave, string providerName, string folderMap, string folderType, string folderZoom)
        {
            this.folderToSave = folderToSave;
            this.providerName = providerName;
            FolderMap = folderMap;
            this.folderType = folderType;
            this.folderZoom = folderZoom;
        }

        public string GeneralFolderToSave => generalFolderToSave = $@"{folderToSave}\{providerName}\{FolderMap}\{folderType}\{folderZoom}";
        public string FolderMap { get; set; }
    }
}