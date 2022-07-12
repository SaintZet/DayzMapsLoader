global using System.Diagnostics;
global using RequestsHub.Domain.DataTypes;
using RequestsHub.Application;
using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.MapsProviders;
using RequestsHub.Infrastructure;
using RequestsHub.Presentation.ConsoleServices;

namespace RequestsHub;

public static class Program
{
    public static void Main(string[] args)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        if (args.Length < 3 && args.Length != 1)
        {
            Console.WriteLine("Invalid args. Use 'help' or '-h' please.");
            return;
        }

        if (args.Length == 1 && string.Equals(args[0], "help", StringComparison.OrdinalIgnoreCase) || string.Equals(args[0], "-h", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine(Documentation.GetDocAboutCommands());
        }

        Args arg = new(args);

        ImageRetrieve imageRetrieve = new(arg.Provider, arg.NameMap, arg.TypeMap, arg.Zoom, arg.PathToSave);
        switch (arg.Command)
        {
            case "GetMap":
                imageRetrieve.GetMap();
                break;

            case "GetMapInParts":
                imageRetrieve.GetMapInParts();
                break;

            case "GetAllMapsInParts":
                imageRetrieve.GetAllMapsInParts();
                break;

            case "GetAllMaps":
                imageRetrieve.GetAllMaps();
                break;

            case "MergePartsAllMaps":
                imageRetrieve.MergePartsAllMaps();
                break;

            case "MergePartsMap":
                imageRetrieve.MergePartsMap();
                break;

            default:
                //TODO: add error message
                throw new Exception("");
        }

        stopWatch.Stop();
        Console.WriteLine($"All time: {stopWatch.Elapsed}");
    }
}