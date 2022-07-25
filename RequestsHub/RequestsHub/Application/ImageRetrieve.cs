using RequestsHub.Application.ImageServices;
using RequestsHub.Domain.Contracts;
using RequestsHub.Infrastructure;

namespace RequestsHub.Application;

internal class ImageRetrieve
{
    internal IMap Map;

    internal ImageRetrieve(IMapProvider mapProvider, MapName mapName, MapType mapType, int mapZoom, string directory)
    {
        DownloadImages.mapName = mapName;
        DownloadImages.mapProvider = mapProvider;
        DownloadImages.mapType = mapType;
        DownloadImages.mapZoom = mapZoom;
        DownloadImages.GeneralSettings = new LocalSave(directory, mapProvider.ToString(), mapType.ToString(), mapZoom.ToString());

        Map = InitializeMap(mapProvider, mapName, mapType, mapZoom);
    }

    internal static Action GetAllMaps => DownloadImages.GetAllMaps;
    internal static Action GetAllMapsInParts => DownloadImages.GetAllMapsInParts;
    internal static Action MergePartsAllMaps => DownloadImages.MergeAllMapsInParts;
    internal static Action<IMap> GetMap => DownloadImages.GetMap;
    internal static Action<IMap> GetMapInParts => DownloadImages.GetMapInParts;
    internal static Action<IMap> MergePartsMap => DownloadImages.MergeMapParts;

    private static IMap InitializeMap(IMapProvider mapProvider, MapName mapName, MapType mapType, int mapZoom)
    {
        Validate.CheckMapAtProvider(mapProvider, mapName);

        IMap map = mapProvider.Maps.SingleOrDefault(x => x.Name == mapName)!;

        Validate.CheckTypeAtMap(map, mapType);
        Validate.CheckZoomAtMap(map, mapZoom);

        return map;
    }
}