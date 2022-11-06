using DayzMapsLoader.Map;
using DayzMapsLoader.MapProviders;
using DayzMapsLoader.MapProviders.Helpers;
using System.Drawing;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Services;

[SupportedOSPlatform("windows")]
public class ImageDownloader
{
    private readonly BaseMapProvider _mapProvider;
    private readonly MergerSquareImages _mergerSquareImages;

    public ImageDownloader(MapProviderName mapProviderName, double qualityImage = 0.5)
    {
        _mapProvider = MapProviderFactory.Create(mapProviderName);
        _mergerSquareImages = new(qualityImage);
    }

    public Bitmap DownloadMap(MapName mapName, MapType mapType, int mapZoom)
    {
        MapInfo mapInfo = _mapProvider.GetMapInfo(mapName, mapType, mapZoom);

        MapParts mapParts = _mapProvider.GetMapParts(mapInfo, mapType, mapZoom);

        return _mergerSquareImages.Merge(mapInfo, mapParts);
    }

    public List<Bitmap> DownloadAllMaps(MapType mapType, int mapZoom)
    {
        List<Bitmap> result = new();

        Parallel.ForEach(_mapProvider.Maps, mapInfo =>
            {
                var image = DownloadMap(mapInfo.Name, mapType, mapZoom);

                result.Add(image);
            }
        );

        return result;
    }

    public byte[,][] DownloadMapInParts(MapName mapName, MapType mapType, int mapZoom)
    {
        MapInfo mapInfo = _mapProvider.GetMapInfo(mapName, mapType, mapZoom);

        MapParts mapParts = _mapProvider.GetMapParts(mapInfo, mapType, mapZoom);

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
        MapInfo mapInfo = _mapProvider.GetMapInfo(mapName, mapType, mapZoom);

        MapParts mapParts = _mapProvider.GetMapParts(mapInfo, mapType, mapZoom);

        Bitmap image = _mergerSquareImages.Merge(mapInfo, mapParts);

        ImageSaver saver = new(pathToSave, _mapProvider, mapInfo, mapType, mapZoom);

        return saver.SaveImageToHardDisk(image, mapInfo);
    }

    public List<string> SaveAllMaps(string pathToSave, MapType mapType, int mapZoom)
    {
        List<string> result = new();

        Parallel.ForEach(_mapProvider.Maps, mapInfo =>
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