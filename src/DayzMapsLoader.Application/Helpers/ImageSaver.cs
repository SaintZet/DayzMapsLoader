using DayzMapsLoader.Domain.Entities.Map;
using System.Drawing;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Application.Helpers;

[SupportedOSPlatform("windows")]
internal static class ImageSaver
{
    private const string _mapName = "!Full_map";

    public static string SaveImageToHardDisk(MapParts source, string pathToFolder, ImageExtension extension)
    {
        int axisY = source.Weight;
        int axisX = source.Height;

        string nameFile, pathToFile;

        for (int y = 0; y < axisY; y++)
        {
            pathToFolder = $@"{pathToFolder}\Horizontal{y}";
            Directory.CreateDirectory(pathToFolder);

            for (int x = 0; x < axisX; x++)
            {
                nameFile = $"({x}.{y}).{extension}";
                pathToFile = Path.Combine(pathToFolder, nameFile);

                source.GetPartOfMap(x, y).Save(pathToFile);
            }
        }

        return pathToFolder;
    }

    internal static string SaveImageToHardDisk(Bitmap image, string pathToFolder, ImageExtension extension)
    {
        Directory.CreateDirectory(pathToFolder);

        string path = $@"{pathToFolder}\{_mapName}.{extension}";

        image.Save(path);
        image.Dispose();

        return path;
    }
}