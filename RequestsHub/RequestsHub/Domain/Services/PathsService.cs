namespace RequestsHub.Domain.Services
{
    internal class PathsService
    {
        public PathsService(string folderToSave, string providerName, string folderMap, string folderType, string folderZoom)
        {
            //TODO: delete this (refactoring) if we dont need this variable at stable version.

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