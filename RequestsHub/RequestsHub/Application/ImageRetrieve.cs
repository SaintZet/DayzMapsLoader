using RequestsHub.Application.ImageServices;
using RequestsHub.Domain.Contracts;
using RequestsHub.Infrastructure;

namespace RequestsHub.Application;

internal class ImageRetrieve
{
    internal ImageRetrieve(IMapProvider mapProvider, MapName mapName, MapType mapType, int mapZoom, string directory)
    {
        DownloadImages.mapProvider = mapProvider;
        DownloadImages.mapName = mapName;
        DownloadImages.mapType = mapType;
        DownloadImages.mapZoom = mapZoom;

        DownloadImages.GeneralSettings = new LocalSave(directory, mapProvider.ToString(), mapType.ToString(), mapZoom.ToString());
    }

    internal static Action<IMap> GetMapInParts => DownloadImages.GetMapInParts;
    internal static Action GetAllMaps => DownloadImages.GetAllMaps;
    internal static Action GetAllMapsInParts => DownloadImages.GetAllMapsInParts;
    internal static Action MergePartsAllMaps => DownloadImages.MergePartsAllMaps;
    internal static Action<IMap> GetMap => DownloadImages.GetMap;
    internal static Action<IMap> MergePartsMap => DownloadImages.MergePartsMap;

    internal IMap InitializeMap()
    {
        Validate.CheckMapAtProvider(mapProvider, mapName);

        IMap map = mapProvider.Maps.SingleOrDefault(x => x.Name == mapName)!;

        Validate.CheckTypeAtMap(map, mapType);
        Validate.CheckZoomAtMap(map, mapZoom);

        return map;
    }
}