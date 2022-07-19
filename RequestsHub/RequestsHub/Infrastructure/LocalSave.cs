using RequestsHub.Domain.Contracts;
using RequestsHub.Presentation.ConsoleServices;

namespace RequestsHub.Infrastructure
{
    internal class LocalSave
    {
        public LocalSave(string generalDirectory, string providerName, string typeMap, string zoom)
        {
            GeneralFolder = generalDirectory;
            ProviderName = providerName;
            TypeFolder = typeMap;
            ZoomFolder = zoom;
        }

        public string GeneralPath => $@"{GeneralFolder}\{ProviderName}\{FolderMap}\{TypeFolder}\{ZoomFolder}";
        public string? FolderMap { get; set; }
        public string GeneralFolder { get; }
        private string ProviderName { get; }
        private string ZoomFolder { get; }
        private string TypeFolder { get; }

        public void SaveImagesToHardDisk(byte[,][] source, ImageExtension ext)
        {
            int axisY = source.GetLength(0);
            int axisX = source.GetLength(1);

            string nameFile, pathToFile, pathToFolder;

            using (ProgressBar progress = new("Save "))
            {
                for (int y = 0; y < axisY; y++)
                {
                    pathToFolder = $@"{GeneralPath}\Horizontal{y}";
                    Directory.CreateDirectory(pathToFolder);

                    for (int x = 0; x < axisX; x++)
                    {
                        nameFile = $"({x}.{y}).{ext}";
                        pathToFile = Path.Combine(pathToFolder, nameFile);

                        File.WriteAllBytes(pathToFile, source[x, y]);
                    }
                }
            }
        }
    }
}