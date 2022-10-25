using DayzMapsLoader.DataTypes;
using DayzMapsLoader.Map;
using DayzMapsLoader.MapProviders;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Services;

[SupportedOSPlatform("windows")]
internal class ImageSaver
{
    private string _folderMap;
    private string _generalFolder;
    private string _zoomFolder;
    private string _typeFolder;
    private string _providerName;

    public ImageSaver(string generalDirectory, IMapProvider provider, MapName mapName, MapType typeMap, int zoom)
    {
        _folderMap = mapName.ToString();
        _generalFolder = generalDirectory;
        _providerName = provider.ToString();
        _typeFolder = typeMap.ToString();
        _zoomFolder = zoom.ToString();
    }

    public string GeneralPath => $@"{_generalFolder}\{_providerName}\{_folderMap}\{_typeFolder}\{_zoomFolder}";

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