using RequestsHub.Application.ImageServices;
using RequestsHub.Domain.Contracts;

namespace RequestsHub.Application;

internal class CommandsManager
{
    private readonly ImageRetrieve imageRetrieve;

    internal CommandsManager(ImageRetrieve imageRetrieve)
    {
        this.imageRetrieve = imageRetrieve;
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

    private void ExucuteMultipleCommands(CommandType command)
    {
        Delegate ExecuteCommand = command switch
        {
            CommandType.GetAllMaps => new Action(ImageRetrieve.GetAllMaps),
            CommandType.GetAllMapsInParts => new Action(ImageRetrieve.GetAllMapsInParts),
            CommandType.MergePartsAllMaps => new Action(ImageRetrieve.MergePartsAllMaps),
            _ => throw new NotImplementedException(),
        };

        ExecuteCommand.DynamicInvoke();
    }

    private void ExecuteSingleCommand(CommandType command)
    {
        Delegate ExecuteCommand = command switch
        {
            CommandType.GetMapInParts => new Action<IMap>(ImageRetrieve.GetMap),
            CommandType.MergePartsMap => new Action<IMap>(ImageRetrieve.GetMapInParts),
            CommandType.MergePartsAllMaps => new Action<IMap>(ImageRetrieve.MergePartsMap),
            _ => throw new NotImplementedException(),
        };

        IMap map = imageRetrieve.InitializeMap();

        ExecuteCommand.DynamicInvoke(map);
    }
}