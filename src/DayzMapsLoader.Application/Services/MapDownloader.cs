using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Domain.Entities.Map;
using System.Drawing;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Application.Services;

[SupportedOSPlatform("windows")]
public class MapDownloader : BaseMapService, IMapDownloader
{
    public MapDownloader(IMapsDbContext mapsDbContext) : base(mapsDbContext)
    {
    }

    public Bitmap DownloadMap(MapName mapName, MapType mapType, int mapZoom)
    {
        var mapInfo = _providerManager.GetMapInfo(mapName, mapType, mapZoom);
        var mapParts = _providerManager.GetMapParts(mapInfo, mapType, mapZoom);

        return _mergerSquareImages.Merge(mapParts, mapInfo.MapExtension);
    }

    public List<Bitmap> DownloadAllMaps(MapType mapType, int mapZoom)
    {
        List<Bitmap> result = new();

        Parallel.ForEach(_providerManager.MapProvider.Maps, mapInfo =>
            {
                var image = DownloadMap(mapInfo.Name, mapType, mapZoom);
                result.Add(image);
            }
        );

        return result;
    }

    public MapParts DownloadMapInParts(MapName mapName, MapType mapType, int mapZoom)
    {
        var mapInfo = _providerManager.GetMapInfo(mapName, mapType, mapZoom);

        return _providerManager.GetMapParts(mapInfo, mapType, mapZoom);
    }

    public List<MapParts> DownloadAllMapsInParts(MapType mapType, int mapZoom)
    {
        List<MapParts> result = new();

        Parallel.ForEach(_providerManager.MapProvider.Maps, mapInfo =>
        {
            var mapInParts = DownloadMapInParts(mapInfo.Name, mapType, mapZoom);
            result.Add(mapInParts);
        });

        return result;
    }
}