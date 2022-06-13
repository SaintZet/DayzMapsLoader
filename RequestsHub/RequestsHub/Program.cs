using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.MapsProviders;
using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.Services.ConsoleServices;
using RequestsHub.Domain.Services;

namespace RequestsHub
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3 && args.Length != 1)
            {
                Console.WriteLine("Invalid args. Use 'help' or '-h' please.");
                return;
            }

            if (args.Length == 1 && string.Equals(args[0], "help", StringComparison.OrdinalIgnoreCase) || string.Equals(args[0], "-h", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(Documentation.GetDocAboutCommands());
            }
            SelectCommand(args[0], ValidateArgs(args));
        }

        private static ArgsFromConsole ValidateArgs(string[] args)
        {
            ArgsFromConsole argsStructure = new ArgsFromConsole();

            if (args.Length == 3)
            {
                ValidateRequiredParams(args, ref argsStructure);
            }
            else if (args.Length > 3 && args.Length < 7)
            {
                ValidateRequiredParams(args, ref argsStructure);
                ValidateOptionParams(args, ref argsStructure);
            }
            else
            {
                throw new ArgumentException("Invalid args. Use 'help' or '-h' please.");
            }
            return argsStructure;
        }

        private static void ValidateRequiredParams(string[] args, ref ArgsFromConsole argsStructure)
        {
            try
            {
                argsStructure.NameProvider = (MapsProvider)Enum.Parse(typeof(MapsProvider), args[1]);
                argsStructure.NameMap = (NameMap)Enum.Parse(typeof(NameMap), args[2]);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid args. Use 'help' or '-h' please.");
            }
        }

        private static void ValidateOptionParams(string[] args, ref ArgsFromConsole argsStructure)
        {
            foreach (string arg in args)
            {
                if (arg.Contains("-zoom:"))
                {
                    string argNew = arg.Replace("-zoom:", "");
                    if (argNew.All(char.IsNumber))
                    {
                        argsStructure.Zoom = int.Parse(argNew);
                        continue;
                    }
                }

                if (Enum.TryParse(arg, out TypeMap _))
                {
                    argsStructure.TypeMap = (TypeMap)Enum.Parse(typeof(TypeMap), arg);
                    continue;
                }

                if (Directory.Exists(arg) || File.Exists(arg))
                {
                    argsStructure.PathToSave = arg;
                }
            }
        }

        private static void SelectCommand(string nameCommand, ArgsFromConsole args)
        {
            IMapProvider mapProvider;
            switch (args.NameProvider)
            {
                case MapsProvider.xam:
                    mapProvider = new Xam();
                    break;

                case MapsProvider.ginfo:
                    mapProvider = new Ginfo();
                    break;

                default:
                    mapProvider = new Xam();
                    break;
            }

            ImageRetrieve imageRetrieve = new(mapProvider, args.NameMap, args.TypeMap, args.Zoom);
            switch (nameCommand)
            {
                case "GetMap":
                    imageRetrieve.GetMap();
                    break;

                case "GetPartsMap":
                    imageRetrieve.GetPartsMap();
                    break;

                case "GetAllMapsInParts":
                    imageRetrieve.GetAllMapsInParts();
                    break;

                case "GetAllMaps":
                    imageRetrieve.GetAllMaps();
                    break;
            }
        }
    }
}