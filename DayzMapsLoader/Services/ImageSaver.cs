using DayzMapsLoader.Map;
using DayzMapsLoader.MapProviders;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Services;

[SupportedOSPlatform("windows")]
internal class ImageSaver
{
    private readonly string _generalPath;

    public ImageSaver(string generalDirectory, BaseMapProvider provider, MapName mapName, MapType typeMap, int zoom)
    {
        _generalPath = $@"{generalDirectory}\{provider}\{mapName}\{typeMap}\{zoom}";
    }

    public void SaveImageToHardDisk(MapParts source, MapExtension ext)
    {
        int axisY = source.Weight;
        int axisX = source.Height;

        string nameFile, pathToFile, pathToFolder;

        for (int y = 0; y < axisY; y++)
        {
            pathToFolder = $@"{_generalPath}\Horizontal{y}";
            Directory.CreateDirectory(pathToFolder);

            for (int x = 0; x < axisX; x++)
            {
                nameFile = $"({x}.{y}).{ext}";
                pathToFile = Path.Combine(pathToFolder, nameFile);

                source.GetPartOfMap(x, y).Save(pathToFile);
            }
        }
    }

    internal string SaveImageToHardDisk(Bitmap image, MapInfo map)
    {
        Directory.CreateDirectory(_generalPath);

        string path = $@"{_generalPath}\{map.Name}.{map.MapExtension}";

        image.Save(path, ImageFormat.Bmp);
        image.Dispose();

        return path;
    }
}