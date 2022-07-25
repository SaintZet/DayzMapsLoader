global using System.Diagnostics;
global using RequestsHub.Domain.DataTypes;
using RequestsHub.Application;
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

        string directory = Validate.PathToSave(arg.PathToSave);
        Console.WriteLine($"Directory to save: {directory}");

        ImageRetrieve imageRetrieve = new(arg.Provider, arg.NameMap, arg.TypeMap, arg.Zoom, directory);

        imageRetrieve.ExecuteCommand(arg.Command);

        stopWatch.Stop();
        Console.WriteLine($"All time: {stopWatch.Elapsed}");

        Console.ReadLine();
    }
}