using RequestsHub.Domain.Contracts;
using RequestsHub.Infrastructure;
using System.Net;

namespace RequestsHub.Application.ImageServices;

internal static class DownloadImages
{
    internal static IMapProvider mapProvider;
    internal static MapName mapName;
    internal static MapType mapType;
    internal static int mapZoom;
    internal static LocalSave? GeneralSettings;
    private const double qualityImage = 0.5;

    public static void GetAllMaps() => Parallel.ForEach(mapProvider.Maps, GetMap);

    public static void MergePartsMap(IMap map)
    {
        Validate.CheckMapAtProvider(mapProvider, map.Name);
        Validate.CheckTypeAtMap(map, mapType);
        Validate.CheckZoomAtMap(map, mapZoom);

        MergeSquareImages mergeImages = new(qualityImage);
        LocalSave save = new(GeneralSettings!)
        {
            FolderMap = map.Name.ToString()
        };

        var image = mergeImages.Merge(save.GeneralPath);
        string pathSave = $@"{save.GeneralPath}\{map.Name}.{map.MapExtension}";

        save.SaveImageToHardDisk(image, pathSave);
    }

    public static void GetAllMapsInParts() => Parallel.ForEach(mapProvider.Maps, GetMapInParts);

    public static void MergePartsAllMaps() => Parallel.ForEach(mapProvider.Maps, MergePartsMap);

    public static void GetMap(IMap map)
    {
        Validate.CheckMapAtProvider(mapProvider, map.Name);
        Validate.CheckTypeAtMap(map, mapType);
        Validate.CheckZoomAtMap(map, mapZoom);

        LocalSave save = new(GeneralSettings!)
        {
            FolderMap = map.Name.ToString()
        };

        string path = $@"{save.GeneralPath}\{map.Name}.{map.MapExtension}";
        Directory.CreateDirectory(save.GeneralPath);

        MergeSquareImages mergeImages = new(qualityImage);
        byte[,][] image = GetImageFromProvider(map);
        var bitmap = mergeImages.Merge(image);

        save.SaveImageToHardDisk(bitmap, path);
    }

    public static void GetMapInParts(IMap map)
    {
        Validate.CheckMapAtProvider(mapProvider, map.Name);
        Validate.CheckTypeAtMap(map, mapType);
        Validate.CheckZoomAtMap(map, mapZoom);

        byte[,][] image = GetImageFromProvider(map);

        LocalSave save = new(GeneralSettings!)
        {
            FolderMap = map.Name.ToString()
        };
        save.SaveImageToHardDisk(image, map.MapExtension);
    }

    private static byte[,][] GetImageFromProvider(IMap map)
    {
        var pair = map.KeyValuePairsSize.SingleOrDefault(x => x.Key == mapZoom);

        int axisY = pair.Value.Height;
        int axisX = pair.Value.Width;

        byte[,][] verticals = new byte[axisY, axisX][];

        WebClient webClient = new WebClient();
        //HttpClient webClient = new();

        var queryBuilder = new QueryBuilder(mapProvider, map, mapType, mapZoom);
        string query;

        int yReversed = axisY - 1;
        for (int y = 0; y < axisY; y++)
        {
            for (int x = 0; x < axisX; x++)
            {
                query = map.IsFirstQuadrant ? query = queryBuilder.GetQuery(x, yReversed) : query = queryBuilder.GetQuery(x, y);
                verticals[x, y] = webClient.DownloadData(query);
            }
            yReversed--;
        }

        return verticals;
    }
}