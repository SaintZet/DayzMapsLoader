using System.Net;
using RequestsHub.Domain.Contracts;
using RequestsHub.Infrastructure;

namespace RequestsHub.Application.ImageServices;

internal class DownloadImages
{
    internal IMapProvider? MapProvider;
    internal MapName MapName;
    internal MapType MapType;
    internal int MapZoom;
    internal LocalSave? GeneralSaveSettings;

    private const double qualityImage = 0.5;

    internal void GetAllMaps() => Parallel.ForEach(MapProvider!.Maps, DownloadMap);

    internal void GetAllMapsInParts() => Parallel.ForEach(MapProvider!.Maps, DownloadMapInParts);

    internal void MergePartsAllMaps() => Parallel.ForEach(MapProvider!.Maps, MergePartsMap);

    internal void DownloadMap(IMap map)
    {
        Validate.CheckMapAtProvider(MapProvider!, map.Name);
        Validate.CheckTypeAtMap(map, MapType);
        Validate.CheckZoomAtMap(map, MapZoom);

        LocalSave save = new(GeneralSaveSettings!)
        {
            FolderMap = map.Name.ToString()
        };

        Directory.CreateDirectory(save.GeneralPath);
        string path = $@"{save.GeneralPath}\{map.Name}.{map.MapExtension}";

        MergeSquareImages mergeImages = new(qualityImage);

        byte[,][] image = GetImageFromProvider(map);

        var bitmap = mergeImages.Merge(image);

        save.SaveImageToHardDisk(bitmap, path);
    }

    internal void DownloadMapInParts(IMap map)
    {
        Validate.CheckMapAtProvider(MapProvider!, map.Name);
        Validate.CheckTypeAtMap(map, MapType);
        Validate.CheckZoomAtMap(map, MapZoom);

        byte[,][] image = GetImageFromProvider(map);

        LocalSave save = new(GeneralSaveSettings!)
        {
            FolderMap = map.Name.ToString()
        };
        save.SaveImageToHardDisk(image, map.MapExtension);
    }

    internal void MergePartsMap(IMap map)
    {
        MergeSquareImages mergeImages = new(qualityImage);

        LocalSave save = new(GeneralSaveSettings!)
        {
            FolderMap = map.Name.ToString()
        };

        var image = mergeImages.Merge(save.GeneralPath);

        string pathSave = $@"{save.GeneralPath}\{map.Name}.{map.MapExtension}";

        save.SaveImageToHardDisk(image, pathSave);
    }

    private byte[,][] GetImageFromProvider(IMap map)
    {
        var pair = map.KeyValuePairsSize.SingleOrDefault(x => x.Key == MapZoom);

        int axisY = pair.Value.Height;
        int axisX = pair.Value.Width;

        byte[,][] verticals = new byte[axisY, axisX][];

        WebClient webClient = new();
        //HttpClient webClient = new();

        var queryBuilder = new QueryBuilder(MapProvider!, map, MapType, MapZoom);
        string query;

        int yReversed = axisY - 1;
        for (int y = 0; y < axisY; y++)
        {
            for (int x = 0; x < axisX; x++)
            {
                query = map.IsFirstQuadrant ? queryBuilder.GetQuery(x, yReversed) : queryBuilder.GetQuery(x, y);
                verticals[x, y] = webClient.DownloadData(query);
            }
            yReversed--;
        }

        return verticals;
    }
}