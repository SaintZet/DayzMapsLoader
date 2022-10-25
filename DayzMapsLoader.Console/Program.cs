using System;

namespace DayzMapsLoader.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //if (args.Length < 3 && args.Length != 1)
            //{
            //    Console.WriteLine("Invalid args. Use 'help' or '-h' please.");
            //    return;
            //}

            //if (args.Length == 1 && string.Equals(args[0], "help", StringComparison.OrdinalIgnoreCase) || string.Equals(args[0], "-h", StringComparison.OrdinalIgnoreCase))
            //{
            //    Console.WriteLine(Documentation.GetDocAboutCommands());
            //}

            //Args arg = new(args);

            //string directory = Validate.PathToSave(arg.PathToSave);
            //Console.WriteLine($"Directory to save: {directory}");

            //ServiceLocator serviceLocator = new(arg.Provider, arg.NameMap, arg.TypeMap, arg.Zoom, directory);
            //serviceLocator.ExecuteCommand(arg.Command);

            Console.ReadLine();
        }
    }
}