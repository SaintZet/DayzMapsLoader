namespace RequestsHub.Infrastructure
{
    internal class PathService
    {
        public PathService(string folderToSave, string providerName, string folderMap, string folderType, string folderZoom)
        {
            GeneralFolder = folderToSave;
            FolderMap = folderMap;

            ProviderName = providerName;
            TypeFolder = folderType;
            ZoomFolder = folderZoom;
        }

        public string GeneralPathToFolderWithFile => $@"{GeneralFolder}\{ProviderName}\{FolderMap}\{TypeFolder}\{ZoomFolder}";
        public string FolderMap { get; set; }
        public string ProviderName { get; }
        public string ZoomFolder { get; }
        public string TypeFolder { get; }
        public string GeneralFolder { get; }
    }
}