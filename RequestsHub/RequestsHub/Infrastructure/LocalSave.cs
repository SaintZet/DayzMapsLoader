using RequestsHub.Domain;
using System.Drawing;
using System.Drawing.Imaging;

namespace RequestsHub.Infrastructure
{
    internal class LocalSaver
    {
        public LocalSaver(LocalSaver original)
        {
            GeneralFolder = original.GeneralFolder;
            ProviderName = original.ProviderName;
            TypeFolder = original.TypeFolder;
            ZoomFolder = original.ZoomFolder;
        }

        public LocalSaver(string generalDirectory, string providerName, string typeMap, string zoom)
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

        public void SaveImageToHardDisk(ImageSet source, ImageExtension ext)
        {
            int axisY = source.Weight;
            int axisX = source.Height;

            string nameFile, pathToFile, pathToFolder;

            for (int y = 0; y < axisY; y++)
            {
                pathToFolder = $@"{GeneralPath}\Horizontal{y}";
                Directory.CreateDirectory(pathToFolder);

                for (int x = 0; x < axisX; x++)
                {
                    nameFile = $"({x}.{y}).{ext}";
                    pathToFile = Path.Combine(pathToFolder, nameFile);

                    source.GetImage(x, y).Save(pathToFile);
                }
            }
        }

        internal void SaveImageToHardDisk(Bitmap image, string pathSave)
        {
            image.Save(pathSave, ImageFormat.Bmp);
            image.Dispose();
        }
    }
}