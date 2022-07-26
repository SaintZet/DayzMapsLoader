using RequestsHub.Application.ImageServices;
using RequestsHub.Domain.Contracts;
using RequestsHub.Infrastructure;

namespace RequestsHub.Application;

internal class ServiceLocator
{
    private readonly IMap map;
    private readonly ImageRetrive imageRetrive;

    internal ServiceLocator(IMapProvider mapProvider, MapName mapName, MapType mapType, int mapZoom, string directory)
    {
        imageRetrive = new ImageRetrive
        {
            MapName = mapName,
            MapProvider = mapProvider,
            MapType = mapType,
            MapZoom = mapZoom,
            GeneralSaveSettings = new LocalSave(directory, mapProvider.ToString(), mapType.ToString(), mapZoom.ToString())
        };

        map = InitializeMap(mapProvider, mapName);
    }

    internal void ExecuteCommand(CommandType command)
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

    internal void ExucuteMultipleCommands(CommandType command)
    {
        Delegate ExecuteCommand = command switch
        {
            CommandType.GetAllMaps => new Action(imageRetrive.GetAllMaps),
            CommandType.GetAllMapsInParts => new Action(imageRetrive.GetAllMapsInParts),
            CommandType.MergePartsAllMaps => new Action(imageRetrive.MergePartsAllMaps),
            //TODO: add error message
            _ => throw new NotImplementedException(),
        };

        ExecuteCommand.DynamicInvoke();
    }

    internal void ExecuteSingleCommand(CommandType command)
    {
        Delegate ExecuteCommand = command switch
        {
            CommandType.GetMap => new Action<IMap>(imageRetrive.DownloadMap),
            CommandType.GetMapInParts => new Action<IMap>(imageRetrive.DownloadMapInParts),
            CommandType.MergePartsMap => new Action<IMap>(imageRetrive.MergePartsMap),
            //TODO: add error message
            _ => throw new NotImplementedException(),
        };

        ExecuteCommand.DynamicInvoke(map);
    }

    private static IMap InitializeMap(IMapProvider mapProvider, MapName mapName)
    {
        Validate.CheckMapAtProvider(mapProvider, mapName);

        IMap map = mapProvider.Maps.SingleOrDefault(x => x.Name == mapName)!;

        return map;
    }
}