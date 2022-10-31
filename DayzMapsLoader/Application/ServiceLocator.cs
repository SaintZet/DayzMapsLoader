using RequestsHub.Application.ImageServices;
using RequestsHub.Domain.Contracts;
using RequestsHub.Infrastructure;

namespace RequestsHub.Application;

internal class ServiceLocator
{
    private readonly IMap map;
    private readonly ImageDownloader imagesDownloader;

    public ServiceLocator(IMapProvider mapProvider, MapName mapName, MapType mapType, int mapZoom, string directory)
    {
        var saveSettings = new LocalSaver(directory, mapProvider.ToString(), mapType.ToString(), mapZoom.ToString());

        imagesDownloader = new ImageDownloader(mapProvider, mapType, mapZoom, saveSettings);

        map = InitializeMap(mapProvider, mapName);
    }

    public void ExecuteCommand(CommandType command) => (command switch
    {
        CommandType.GetAllMaps => new Action<IMap>(_ => imagesDownloader.GetAllMaps()),
        CommandType.GetAllMapsInParts => _ => imagesDownloader.GetAllMapsInParts(),
        CommandType.MergePartsAllMaps => _ => imagesDownloader.MergePartsAllMaps(),
        CommandType.GetMap => imagesDownloader.DownloadMap,
        CommandType.GetMapInParts => imagesDownloader.DownloadMapInParts,
        CommandType.MergePartsMap => imagesDownloader.MergePartsMap,
        _ => throw new NotImplementedException()
    })(map);

    private static IMap InitializeMap(IMapProvider mapProvider, MapName mapName)
    {
        return mapProvider.Maps.SingleOrDefault(x => x.Name == mapName)!;
    }
}