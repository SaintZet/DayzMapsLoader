using DayzMapsLoader.Application.Abstractions;
using DayzMapsLoader.Application.Managers;
using DayzMapsLoader.Domain.Entities.Map;
using DayzMapsLoader.Domain.Entities.MapProvider;
using System.Drawing;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Application.Services;

[SupportedOSPlatform("windows")]
public class MapDownloader : IMapDownloader
{
    private readonly IMapsDbContext _mapsDbContext;

    private readonly ProviderManager _providerManager = new();
    private readonly MergerSquareImages _mergerSquareImages = new(0.5);

    private MapProviderName _mapProviderName;

    public MapDownloader(IMapsDbContext mapsDbContext)
    {
        _mapsDbContext = mapsDbContext;
    }

    public MapProviderName MapProviderName
    {
        get => _mapProviderName;
        set
        {
            if (value == _mapProviderName) return;

            MapProvider mapProviderEntity = _mapsDbContext.GetMapProvider(value);

            _providerManager.MapProviderEntity = mapProviderEntity;
            _mapProviderName = value;
        }
    }
    public double QualityImage
    {
        get => _mergerSquareImages.DpiImprovementPercent;
        set => _mergerSquareImages.DpiImprovementPercent = value;
    }

    public Bitmap DownloadMap(MapName mapName, MapType mapType, int mapZoom)
    {
        MapInfo mapInfo = _providerManager.GetMapInfo(mapName, mapType, mapZoom);

        MapParts mapParts = _providerManager.GetMapParts(mapInfo, mapType, mapZoom);

        return _mergerSquareImages.Merge(mapInfo, mapParts);
    }

    public List<Bitmap> DownloadAllMaps(MapType mapType, int mapZoom)
    {
        List<Bitmap> result = new();

        Parallel.ForEach(_providerManager.MapProviderEntity.Maps, mapInfo =>
            {
                var image = DownloadMap(mapInfo.Name, mapType, mapZoom);

                result.Add(image);
            }
        );

        return result;
    }

    public byte[,][] DownloadMapInParts(MapName mapName, MapType mapType, int mapZoom)
    {
        MapInfo mapInfo = _providerManager.GetMapInfo(mapName, mapType, mapZoom);

        MapParts mapParts = _providerManager.GetMapParts(mapInfo, mapType, mapZoom);

        return mapParts.GetRawMapParts();
    }

    public List<byte[,][]> DownloadAllMapsInParts(MapType mapType, int mapZoom)
    {
        List<byte[,][]> result = new();

        //Parallel.ForEach(_mapProvider.Maps, mapInfo =>
        //{
        //    var mapInParts = DownloadMapInParts(mapInfo.Name, mapType, mapZoom);

        //    result.Add(mapInParts);
        //}
        //);

        return result;
    }

    public string SaveMap(string pathToSave, MapName mapName, MapType mapType, int mapZoom)
    {
        MapInfo mapInfo = _providerManager.GetMapInfo(mapName, mapType, mapZoom);

        MapParts mapParts = _providerManager.GetMapParts(mapInfo, mapType, mapZoom);

        Bitmap image = _mergerSquareImages.Merge(mapInfo, mapParts);

        ImageSaver saver = new(pathToSave, _providerManager, mapInfo, mapType, mapZoom);

        return saver.SaveImageToHardDisk(image, mapInfo);
    }

    public List<string> SaveAllMaps(string pathToSave, MapType mapType, int mapZoom)
    {
        List<string> result = new();

        Parallel.ForEach(_providerManager.MapProviderEntity.Maps, mapInfo =>
            {
                var path = SaveMap(pathToSave, mapInfo.Name, mapType, mapZoom);

                result.Add(path);
            }
        );

        return result;
    }

    //public void GetAllMaps() => Parallel.ForEach(_mapProvider!.Maps, DownloadMap);

    //public void GetAllMapsInParts() => Parallel.ForEach(_mapProvider!.Maps, DownloadMapInParts);

    //public void MergePartsAllMaps() => Parallel.ForEach(_mapProvider!.Maps, MergePartsMap);

    //public void DownloadMapInParts(MapInfo map)
    //{
    //    Validate.CheckMapAtProvider(_mapProvider!, map.Name);
    //    Validate.CheckTypeAtMap(map, _mapType);
    //    Validate.CheckZoomAtMap(map, _mapZoom);

    // var image = GetImageFromProvider(map);

    //    ImageSaver save = new(_generalSaveSettings!)
    //    {
    //        FolderMap = map.Name.ToString()
    //    };
    //    save.SaveImageToHardDisk(image, map.MapExtension);
    //}

    //public void MergePartsMap(MapInfo map)
    //{
    //    MergerSquareImages mergeImages = new(_qualityImage);

    // ImageSaver save = new(_generalSaveSettings!) { FolderMap = map.Name.ToString() };

    // var image = mergeImages.Merge(save.GeneralPath);

    // string pathSave = $@"{save.GeneralPath}\{map.Name}.{map.MapExtension}";

    //    save.SaveImageToHardDisk(image, pathSave);
    //}
}