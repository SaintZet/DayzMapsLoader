using RequestsHub.Application.Services.ImageServices;
using RequestsHub.Domain.Contracts;
using RequestsHub.Infrastructure;
using RequestsHub.Presentation.ConsoleServices;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Linq;

namespace RequestsHub.Application;

public class ImageRetrieve
{
    private readonly IMapProvider mapProvider;
    private readonly int zoom;
    private readonly TypeMap typeMap;
    private readonly LocalSave localSave;
    private readonly Dictionary<CommandType, Action> commands = new();

    internal ImageRetrieve(IMapProvider mapProvider, MapName nameMap, TypeMap typeMap, int zoom, string directory)
    {
        this.mapProvider = mapProvider;
        this.typeMap = typeMap;
        this.zoom = zoom;

        string generalDirectory = Validate.PathToSave(directory);
        string providerName = Enum.GetName(typeof(MapProvider), mapProvider.Name) ?? default!;

        localSave = new LocalSave(generalDirectory, providerName, typeMap.ToString(), zoom.ToString());
        Console.WriteLine($"Directory to save: {localSave.GeneralFolder}");

        commands.Add(CommandType.GetAllMaps, new Action(GetAllMaps));
        commands.Add(CommandType.GetAllMapsInParts, new Action(GetAllMapsInParts));
        //commands.Add(CommandType.GetMap, new Action(GetMap));
        //commands.Add(CommandType.GetMapInParts, new Action(GetMapInParts));
        //commands.Add(CommandType.MergePartsMap, new Action(MergePartsMap));
        commands.Add(CommandType.MergePartsAllMaps, new Action(MergePartsAllMaps));
    }

    public void ExecuteCommand(CommandType command) => commands[command]();

    private void GetAllMaps() => mapProvider.Maps.ForEach(x => GetMap(x));

    private void MergePartsMap(IMap map)
    {
        Validate.CheckMapAtProvider(mapProvider, map.MapName);
        Validate.CheckTypeAtMap(map, typeMap);
        Validate.CheckZoomAtMap(map, zoom);

        localSave.FolderMap = map.MapName.ToString();
        string pathSource = $"{localSave.GeneralPath}";

        string pathSave = $@"{localSave.GeneralPath}\{map.MapName}.{map.MapExtension}";
        Directory.CreateDirectory(pathSave);

        new MergeImages().MergeAndSave(pathSource, pathSave);
    }

    private void GetAllMapsInParts() => mapProvider.Maps.ForEach(x => GetMapInParts(x));

    private void GetMap(IMap map)
    {
        Validate.CheckMapAtProvider(mapProvider, map.MapName);
        Validate.CheckTypeAtMap(map, typeMap);
        Validate.CheckZoomAtMap(map, zoom);

        byte[,][] image = GetImageFromProvider(map);

        localSave.FolderMap = map.MapName.ToString();
        string path = $@"{localSave.GeneralPath}\{map.MapName}.{map.MapExtension}";
        Directory.CreateDirectory(localSave.GeneralPath);

        new MergeImages().MergeAndSave(image, path);
    }

    private void GetMapInParts(IMap map)
    {
        Validate.CheckMapAtProvider(mapProvider, map.MapName);
        Validate.CheckTypeAtMap(map, typeMap);
        Validate.CheckZoomAtMap(map, zoom);

        byte[,][] image = GetImageFromProvider(map);

        localSave.FolderMap = map.MapName.ToString();

        Stopwatch stopWatch = new();
        stopWatch.Start();

        localSave.SaveImagesToHardDisk(image, map.MapExtension);

        stopWatch.Stop();
        Console.WriteLine("time: {0}", stopWatch.Elapsed);
    }

    private void MergePartsAllMaps() => mapProvider.Maps.ForEach(x => MergePartsMap(x));

    private byte[,][] GetImageFromProvider(IMap map)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        var axis = LookupAxis(map);
        int axisY = axis.y;
        int axisX = axis.y;

        byte[,][] verticals = new byte[axisY, axisX][];

        WebClient webClient = new();

        var queryBuilder = new QueryBuilder(mapProvider, map, typeMap, zoom);
        string query;

        using (ProgressBar progress = new($"Download {map.MapName}".PadRight(20)))
        {
            int yReversed = axisY - 1;
            for (int y = 0; y < axisY; y++)
            {
                for (int x = 0; x < axisX; x++)
                {
                    query = map.IsFirstQuadrant ? query = queryBuilder.GetQuery(x, yReversed) : query = queryBuilder.GetQuery(x, y);

                    verticals[x, y] = webClient.DownloadData(query);

                    progress.Report((double)(y * axisX + x) / (axisX * axisY));
                }
                yReversed--;
            }
        }

        stopWatch.Stop();
        Console.Write(" time: {0} ", stopWatch.Elapsed);

        return verticals;
    }

    private (int x, int y) LookupAxis(IMap map)
    {
        foreach (var item in map.KeyValuePairsSize.Where(item => item.Key == zoom))
        {
            return (x: item.Value.Width, y: item.Value.Height);
        }
        //TODO: message.
        throw new Exception();
    }
}