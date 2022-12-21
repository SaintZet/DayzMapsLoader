using DayzMapsLoader.Domain.Entities.Map;
using System.Drawing;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Application.Managers;

[SupportedOSPlatform("windows")]
internal class ImageSaver
{
    private readonly string _generalPath;
    private readonly MapExtension _mapExtension;

    public ImageSaver(string generalDirectory, ProviderManager provider, MapInfo mapInfo, MapType typeMap, int zoom)
    {
        //TODO: Check end string with \ in general directory.
        _generalPath = $@"{generalDirectory}\{provider}\{mapInfo.Name}\{typeMap}\{mapInfo.Version}\{zoom}";
        _mapExtension = mapInfo.MapExtension;
    }

    public void SaveImageToHardDisk(MapParts source)
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
                nameFile = $"({x}.{y}).{_mapExtension}";
                pathToFile = Path.Combine(pathToFolder, nameFile);

                source.GetPartOfMap(x, y).Save(pathToFile);
            }
        }
    }

    internal string SaveImageToHardDisk(Bitmap image, MapInfo map)
    {
        Directory.CreateDirectory(_generalPath);

        //TODO: Change .jpg  to variable.
        string path = $@"{_generalPath}\{map.Name}.jpg";

        image.Save(path);
        image.Dispose();

        return path;
    }
}