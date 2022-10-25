using DayzMapsLoader.Contracts;
using DayzMapsLoader.DataTypes;
using System.Net;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Services;

[SupportedOSPlatform("windows")]
internal class ImageDownloader
{
    private const double _qualityImage = 0.5;

    private readonly IMapProvider? _mapProvider;
    private readonly MapType _mapType;
    private readonly int _mapZoom;
    private readonly ImageSaver? _generalSaveSettings;

    public ImageDownloader(IMapProvider? mapProvider, MapType mapType, int mapZoom, ImageSaver? generalSaveSettings)
    {
        _mapProvider = mapProvider;
        _mapType = mapType;
        _mapZoom = mapZoom;
        _generalSaveSettings = generalSaveSettings;
    }

    public void GetAllMaps() => Parallel.ForEach(_mapProvider!.Maps, DownloadMap);

    public void GetAllMapsInParts() => Parallel.ForEach(_mapProvider!.Maps, DownloadMapInParts);

    public void MergePartsAllMaps() => Parallel.ForEach(_mapProvider!.Maps, MergePartsMap);

    public void DownloadMap(IMap map)
    {
        Validate.CheckMapAtProvider(_mapProvider!, map.Name);
        Validate.CheckTypeAtMap(map, _mapType);
        Validate.CheckZoomAtMap(map, _mapZoom);

        ImageSaver save = new(_generalSaveSettings!)
        {
            FolderMap = map.Name.ToString()
        };

        Directory.CreateDirectory(save.GeneralPath);
        string path = $@"{save.GeneralPath}\{map.Name}.{map.MapExtension}";

        MergerSquareImages mergeImages = new(_qualityImage);

        var image = GetImageFromProvider(map);

        var bitmap = mergeImages.Merge(image);

        save.SaveImageToHardDisk(bitmap, path);
    }

    public void DownloadMapInParts(IMap map)
    {
        Validate.CheckMapAtProvider(_mapProvider!, map.Name);
        Validate.CheckTypeAtMap(map, _mapType);
        Validate.CheckZoomAtMap(map, _mapZoom);

        var image = GetImageFromProvider(map);

        ImageSaver save = new(_generalSaveSettings!)
        {
            FolderMap = map.Name.ToString()
        };
        save.SaveImageToHardDisk(image, map.MapExtension);
    }

    public void MergePartsMap(IMap map)
    {
        MergerSquareImages mergeImages = new(_qualityImage);

        ImageSaver save = new(_generalSaveSettings!)
        {
            FolderMap = map.Name.ToString()
        };

        var image = mergeImages.Merge(save.GeneralPath);

        string pathSave = $@"{save.GeneralPath}\{map.Name}.{map.MapExtension}";

        save.SaveImageToHardDisk(image, pathSave);
    }

    private ImageSet GetImageFromProvider(IMap map)
    {
        var pair = map.ZoomLevelRatioToSize.SingleOrDefault(x => x.Key == _mapZoom);

        int axisY = pair.Value.Height;
        int axisX = pair.Value.Width;

        ImageSet verticals = new ImageSet(axisY, axisX);

        WebClient webClient = new();
        //HttpClient webClient = new();

        var queryBuilder = new QueryBuilder(_mapProvider!, map, _mapType, _mapZoom);
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