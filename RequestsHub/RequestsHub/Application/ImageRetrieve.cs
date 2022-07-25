using RequestsHub.Application.Services.ImageServices;
using RequestsHub.Domain.Contracts;
using RequestsHub.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace RequestsHub.Application;

public class ImageRetrieve
{
    //TODO: Delete before merge with master.
    private const double qualityImage = 0.5;
    private readonly IMapProvider mapProvider;
    private readonly MapName nameMap;
    private readonly TypeMap typeMap;
    private readonly int zoom;
    private LocalSave generalSettings;

    internal ImageRetrieve(IMapProvider mapProvider, MapName nameMap, TypeMap typeMap, int zoom, string directory)
    {
        this.mapProvider = mapProvider;
        this.nameMap = nameMap;
        this.typeMap = typeMap;
        this.zoom = zoom;

        generalSettings = new LocalSave(directory, mapProvider.ToString(), typeMap.ToString(), zoom.ToString());
    }

    public void ExecuteCommand(CommandType command)
    {
        switch (command)
        {
            case CommandType.GetAllMaps:
            case CommandType.GetAllMapsInParts:
            case CommandType.MergePartsAllMaps:
                ExucuteMultipleCommands(command);
                break;

            case CommandType.GetMap:
            case CommandType.GetMapInParts:
            case CommandType.MergePartsMap:
                ExecuteSingleCommand(command);
                break;

            default:
                throw new NotImplementedException();
        }
    }

    public void GetAllMaps() => Parallel.ForEach(mapProvider.Maps, GetMap);

    private void ExecuteSingleCommand(CommandType command)
    {
        Delegate ExecuteCommand = command switch
        {
            CommandType.GetMapInParts => new Action<IMap>(GetMap),
            CommandType.MergePartsMap => new Action<IMap>(GetMapInParts),
            CommandType.MergePartsAllMaps => new Action<IMap>(MergePartsMap),
            _ => throw new NotImplementedException(),
        };

        IMap map = InitializeMap();

        ExecuteCommand.DynamicInvoke(map);
    }

    private IMap InitializeMap()
    {
        Validate.CheckMapAtProvider(mapProvider, nameMap);

        IMap map = mapProvider.Maps.SingleOrDefault(x => x.Name == nameMap)!;

        Validate.CheckTypeAtMap(map, typeMap);
        Validate.CheckZoomAtMap(map, zoom);

        return map;
    }

    private void ExucuteMultipleCommands(CommandType command)
    {
        Delegate ExecuteCommand = command switch
        {
            CommandType.GetAllMaps => new Action(GetAllMaps),
            CommandType.GetAllMapsInParts => new Action(GetAllMapsInParts),
            CommandType.MergePartsAllMaps => new Action(MergePartsAllMaps),
            _ => throw new NotImplementedException(),
        };

        ExecuteCommand.DynamicInvoke();
    }

    private void MergePartsMap(IMap map)
    {
        Validate.CheckMapAtProvider(mapProvider, map.Name);
        Validate.CheckTypeAtMap(map, typeMap);
        Validate.CheckZoomAtMap(map, zoom);

        MergeSquareImages mergeImages = new(qualityImage);
        LocalSave save = new(generalSettings)
        {
            FolderMap = map.Name.ToString()
        };

        var image = mergeImages.Merge(save.GeneralPath);
        string pathSave = $@"{save.GeneralPath}\{map.Name}.{map.MapExtension}";

        save.SaveImageToHardDisk(image, pathSave);
    }

    private void GetAllMapsInParts() => Parallel.ForEach(mapProvider.Maps, GetMapInParts);

    private void GetMap(IMap map)
    {
        Validate.CheckMapAtProvider(mapProvider, map.Name);
        Validate.CheckTypeAtMap(map, typeMap);
        Validate.CheckZoomAtMap(map, zoom);

        LocalSave save = new(generalSettings)
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

    private void GetMapInParts(IMap map)
    {
        Validate.CheckMapAtProvider(mapProvider, map.Name);
        Validate.CheckTypeAtMap(map, typeMap);
        Validate.CheckZoomAtMap(map, zoom);

        byte[,][] image = GetImageFromProvider(map);

        LocalSave save = new(generalSettings)
        {
            FolderMap = map.Name.ToString()
        };
        save.SaveImageToHardDisk(image, map.MapExtension);
    }

    private void MergePartsAllMaps() => Parallel.ForEach(mapProvider.Maps, MergePartsMap);

    private byte[,][] GetImageFromProvider(IMap map)
    {
        var pair = map.KeyValuePairsSize.SingleOrDefault(x => x.Key == zoom);

        int axisY = pair.Value.Height;
        int axisX = pair.Value.Width;

        byte[,][] verticals = new byte[axisY, axisX][];

        WebClient webClient = new WebClient();
        //HttpClient webClient = new();

        var queryBuilder = new QueryBuilder(mapProvider, map, typeMap, zoom);
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

    //private async Task<byte[]> GetPeaceAsync(int x, int y, string query)
    //{
    //    return await webClient.GetByteArrayAsync(query);
    //}
}