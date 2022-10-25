using DayzMapsLoader.DataTypes;
using DayzMapsLoader.Map;
using DayzMapsLoader.MapProviders;
using DayzMapsLoader.MapProviders.Helpers;
using System.Drawing;
using System.Net;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Services;

[SupportedOSPlatform("windows")]
public class ImageDownloader
{
    private readonly IMapProvider _mapProvider;
    private readonly MergerSquareImages _mergerSquareImages;

    public ImageDownloader(MapProviderName mapProviderName, double qualityImage = 0.5)
    {
        _mapProvider = MapProviderFactory.Create(mapProviderName);
        _mergerSquareImages = new(qualityImage);
    }

    //public void GetAllMaps() => Parallel.ForEach(_mapProvider!.Maps, DownloadMap);

    //public void GetAllMapsInParts() => Parallel.ForEach(_mapProvider!.Maps, DownloadMapInParts);

    //public void MergePartsAllMaps() => Parallel.ForEach(_mapProvider!.Maps, MergePartsMap);

    public Bitmap DownloadMap(MapName mapName, MapType mapType, int mapZoom)
    {
        MapInfo map = InitializeMap(mapName, mapType, mapZoom);

        ImageSet image = GetImageFromProvider(map, mapType, mapZoom);

        return _mergerSquareImages.Merge(image);
    }

    public void SaveMap(string pathToSave, MapName mapName, MapType mapType, int mapZoom)
    {
        MapInfo map = InitializeMap(mapName, mapType, mapZoom);

        ImageSet image = DownloadMap(mapName, mapType, mapZoom);

        ImageSaver save = new(pathToSave, _mapProvider, mapName, mapType, mapZoom);

        Directory.CreateDirectory(save.GeneralPath);
        string path = $@"{save.GeneralPath}\{map.Name}.{map.MapExtension}";

        image.Save(pathSave, ImageFormat.Bmp);
        image.Dispose();
    }

    private MapInfo InitializeMap(MapName mapName, MapType mapType, int mapZoom)
    {
        Validate.CheckMapAtProvider(_mapProvider, mapName);

        var map = _mapProvider.Maps.SingleOrDefault(x => x.Name == mapName);

        Validate.CheckTypeAtMap(map, mapType);
        Validate.CheckZoomAtMap(map, mapZoom);

        return map;
    }

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

    private ImageSet GetImageFromProvider(MapInfo map, MapType mapType, int mapZoom)
    {
        var pair = map.ZoomLevelRatioToSize.SingleOrDefault(x => x.Key == mapZoom);

        int axisY = pair.Value.Height;
        int axisX = pair.Value.Width;

        ImageSet verticals = new ImageSet(axisY, axisX);

        WebClient webClient = new();
        //HttpClient webClient = new();

        var queryBuilder = new QueryBuilder(_mapProvider!, map, mapType, mapZoom);
        string query;

        int yReversed = axisY - 1;
        for (int y = 0; y < axisY; y++)
        {
            for (int x = 0; x < axisX; x++)
            {
                query = map.IsFirstQuadrant ? queryBuilder.GetQuery(x, yReversed) : queryBuilder.GetQuery(x, y);
                verticals.SetImage(x, y, new ProviderImage(webClient.DownloadData(query)));
            }
            yReversed--;
        }

        return verticals;
    }
}