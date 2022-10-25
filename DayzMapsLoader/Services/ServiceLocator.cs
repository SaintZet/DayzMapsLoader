//using DayzMapsLoader.Map;
//using DayzMapsLoader.MapProviders;
//using System.Runtime.Versioning;

//namespace DayzMapsLoader.Services;

//[SupportedOSPlatform("windows")]
//internal class ServiceLocator
//{
//    private readonly MapInfo _map;
//    private readonly ImageDownloader _imagesDownloader;

// public ServiceLocator(IMapProvider mapProvider, MapName mapName, MapType mapType, int mapZoom,
// string directory) { //var saveSettings = new ImageSaver(directory, mapProvider.ToString(), //
// mapType.ToString(), mapZoom.ToString()); //_imagesDownloader = new ImageDownloader(mapProvider,
// mapType, mapZoom, saveSettings);

// //_map = InitializeMap(mapProvider, mapName); }

// //public void ExecuteCommand(CommandType command) => (command switch //{ //
// CommandType.GetAllMaps => new Action<IMap>(_ => _imagesDownloader.GetAllMaps()), //
// CommandType.GetAllMapsInParts => _ => _imagesDownloader.GetAllMapsInParts(), //
// CommandType.MergePartsAllMaps => _ => _imagesDownloader.MergePartsAllMaps(), //
// CommandType.GetMap => _imagesDownloader.DownloadMap, // CommandType.GetMapInParts =>
// _imagesDownloader.DownloadMapInParts, // CommandType.MergePartsMap =>
// _imagesDownloader.MergePartsMap, // _ => throw new NotImplementedException() //})(_map);

// private static MapInfo InitializeMap(IMapProvider mapProvider, MapName mapName) {
// Validate.CheckMapAtProvider(mapProvider, mapName);

//        return mapProvider.Maps.SingleOrDefault(x => x.Name == mapName)!;
//    }
//}