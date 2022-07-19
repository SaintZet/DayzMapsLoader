using RequestsHub.Application.Services.ImageServices;
using RequestsHub.Domain.Contracts;
using RequestsHub.Infrastructure;
using RequestsHub.Presentation.ConsoleServices;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace RequestsHub.Application;

public class ImageRetrieve
{
    private readonly IMapProvider mapProvider;
    private readonly int zoom;
    private readonly TypeMap typeMap;
    private readonly PathService pathsService;
    private readonly Dictionary<CommandType, Action> commands = new();

    [NotNull]
    private IMap currentMap = default!;

    internal ImageRetrieve(IMapProvider mapProvider, MapName nameMap, TypeMap typeMap, int zoom, string pathsToSave)
    {
        this.mapProvider = mapProvider;
        this.typeMap = typeMap;
        this.zoom = zoom;

        pathsService = InitializePathService(pathsToSave, nameMap);
        Console.WriteLine($"Directory to save: {pathsService.GeneralFolder}");

        commands.Add(CommandType.GetAllMaps, new Action(GetAllMaps));
        commands.Add(CommandType.GetAllMapsInParts, new Action(GetAllMapsInParts));
        commands.Add(CommandType.GetMap, new Action(GetMap));
        commands.Add(CommandType.GetMapInParts, new Action(GetMapInParts));
        commands.Add(CommandType.MergePartsMap, new Action(MergePartsMap));
        commands.Add(CommandType.MergePartsAllMaps, new Action(MergePartsAllMaps));
    }

    public void ExecuteCommand(CommandType command)
    {
        commands[command]();
    }

    private void GetAllMaps()
    {
        foreach (IMap map in mapProvider.Maps)
        {
            currentMap = Validate.CheckMapAtProvider(mapProvider, map.MapName);
            pathsService.FolderMap = currentMap.MapName.ToString();
            GetMap();
        }
    }

    private void MergePartsMap()
    {
        string pathSource = $"{pathsService.GeneralPathToFolderWithFile}";

        Directory.CreateDirectory(pathsService.GeneralPathToFolderWithFile);
        string pathSave = $@"{pathsService.GeneralPathToFolderWithFile}\{currentMap.MapName}.{currentMap.MapExtension}";

        new MergeImages().MergeAndSave(pathSource, pathSave);
    }

    private void GetAllMapsInParts()
    {
        foreach (var map in mapProvider.Maps)
        {
            currentMap = Validate.CheckMapAtProvider(mapProvider, map.MapName);
            pathsService.FolderMap = currentMap.MapName.ToString();
            GetMapInParts();
        }
    }

    private void GetMap()
    {
        byte[,][] source = GetImageFromProvider();

        Directory.CreateDirectory(pathsService.GeneralPathToFolderWithFile);
        string path = $@"{pathsService.GeneralPathToFolderWithFile}\{currentMap.MapName}.{currentMap.MapExtension}";

        new MergeImages().MergeAndSave(source, path);
    }

    private void GetMapInParts()
    {
        byte[,][] source = GetImageFromProvider();
        SaveImagesToHardDisk(source);
    }

    private void MergePartsAllMaps()
    {
        foreach (IMap map in mapProvider.Maps)
        {
            currentMap = Validate.CheckMapAtProvider(mapProvider, map.MapName);
            pathsService.FolderMap = currentMap.MapName.ToString();
            MergePartsMap();
        }
    }

    private PathService InitializePathService(string pathsToSave, MapName nameMap)
    {
        pathsToSave = Validate.PathToSave(pathsToSave);
        currentMap = Validate.CheckMapAtProvider(mapProvider, nameMap);

        Validate.CheckTypeAtMap(currentMap, typeMap);
        Validate.CheckZoomAtMap(currentMap, zoom);

        string providerName = Enum.GetName(typeof(MapProvider), mapProvider.Name) ?? default!;

        return new PathService(pathsToSave, providerName, currentMap.MapName.ToString(), typeMap.ToString(), zoom.ToString());
    }

    private void SaveImagesToHardDisk(byte[,][] source)
    {
        Stopwatch stopWatch = new();
        stopWatch.Start();

        var axis = LookupAxis();
        int axisY = axis.y;
        int axisX = axis.y;

        string nameFile, pathToFile, pathToFolder;
        Console.Write("Save ");
        using (ProgressBar progress = new())
        {
            for (int y = 0; y < axisY; y++)
            {
                pathToFolder = $@"{pathsService.GeneralPathToFolderWithFile}\Horizontal{y}";
                Directory.CreateDirectory(pathToFolder);

                for (int x = 0; x < axisX; x++)
                {
                    nameFile = $"({x}.{y}).{currentMap.MapExtension}";
                    pathToFile = Path.Combine(pathToFolder, nameFile);

                    File.WriteAllBytes(pathToFile, source[x, y]);
                }
            }
        }
        stopWatch.Stop();
        Console.WriteLine("time: {0}", stopWatch.Elapsed);
    }

    private byte[,][] GetImageFromProvider()
    {
        Console.Write($"Download {currentMap.MapName}".PadRight(20));
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        var axis = LookupAxis();
        int axisY = axis.y;
        int axisX = axis.y;

        byte[,][] verticals = new byte[axisY, axisX][];

        WebClient webClient = new();

        var queryBuilder = new QueryBuilder(mapProvider, currentMap, typeMap, zoom);
        string query;

        using (ProgressBar progress = new())
        {
            int yReversed = axisY - 1;
            for (int y = 0; y < axisY; y++)
            {
                for (int x = 0; x < axisX; x++)
                {
                    if (currentMap.IsFirstQuadrant)
                    {
                        query = queryBuilder.GetQuery(x, yReversed);
                    }
                    else
                    {
                        query = queryBuilder.GetQuery(x, y);
                    }

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

    private (int x, int y) LookupAxis()
    {
        //TODO: use linq;
        KeyValuePair<int, MapSize> keyValuePair = default;
        foreach (var item in currentMap.KeyValuePairsSize)
        {
            if (item.Key == zoom)
            {
                keyValuePair = item;
            }
        }

        return (x: keyValuePair.Value.Width, y: keyValuePair.Value.Height);
    }
}