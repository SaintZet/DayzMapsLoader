using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Helpers;
using DayzMapsLoader.Domain.Entities.Map;

namespace DayzMapsLoader.Application.Services;

public class MapSaver : BaseMapService, IMapSaver
{
    public MapSaver(IMapsDbContext mapsDbContext) : base(mapsDbContext)
    {
    }

    public ImageExtension ImageExtansionForSave { get; set; } = ImageExtension.jpg;

    public string SaveMap(string pathToSave, MapName mapName, MapType mapType, int mapZoom)
    {
        var mapInfo = _providerManager.GetMapInfo(mapName, mapType, mapZoom);
        var mapParts = _providerManager.GetMapParts(mapInfo, mapType, mapZoom);
        var image = _imageMerger.Merge(mapParts, mapInfo.MapExtension);
        string path = $@"{pathToSave}\{_providerManager}\{mapInfo.Name}\{mapType}\{mapInfo.Version}\{mapZoom}";

        return ImageSaver.SaveImageToHardDisk(image, path, ImageExtansionForSave);
    }

    public string SaveMapInParts(string pathToSave, MapName mapName, MapType mapType, int mapZoom)
    {
        var mapInfo = _providerManager.GetMapInfo(mapName, mapType, mapZoom);
        var mapParts = _providerManager.GetMapParts(mapInfo, mapType, mapZoom);
        string path = $@"{pathToSave}\{_providerManager}\{mapInfo.Name}\{mapType}\{mapInfo.Version}\{mapZoom}";

        return ImageSaver.SaveImageToHardDisk(mapParts, path, ImageExtansionForSave);
    }

    public List<string> SaveAllMaps(string pathToSave, MapType mapType, int mapZoom)
    {
        List<string> result = new();

        Parallel.ForEach(_providerManager.MapProvider.Maps, mapInfo =>
        {
            var path = SaveMap(pathToSave, mapInfo.Name, mapType, mapZoom);
            result.Add(path);
        });

        return result;
    }
}