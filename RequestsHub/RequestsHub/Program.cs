using RequestsHub.Domain.Services;
using RequestsHub.Domain.DataTypes;

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
                Console.WriteLine(DocumentationManager.GetDocAboutCommands());
            }

            MarshallerArgs(args);
        }

        private static void MarshallerArgs(string[] args)
        {
            Args argsStructure = new Args();
            switch (args[0])
            {
                case "GetImages" when args.Length == 3:
                    ValidateRequiredParams(args, ref argsStructure);
                    GetImages(ref argsStructure);
                    break;

                case "GetImages" when args.Length > 3 && args.Length < 8:
                    ValidateRequiredParams(args, ref argsStructure);
                    ValidateOptionParams(args, ref argsStructure);
                    GetImages(ref argsStructure);
                    break;

                default:
                    Console.WriteLine("Invalid args. Use 'help' or '-h' please.");
                    return;
            }
        }

        private static void ValidateOptionParams(string[] args, ref Args argsStructure)
        {
            foreach (string arg in args)
            {
                if (arg.Contains("-zoom:"))
                {
                    string argNew = arg.Replace("-zoom:", "");
                    if (IsNumeric(argNew))
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

                if (Enum.TryParse(arg, out Direction _))
                {
                    argsStructure.Direction = (Direction)Enum.Parse(typeof(Direction), arg);
                    continue;
                }
                if (Directory.Exists(arg) || File.Exists(arg))
                {
                    argsStructure.PathToSave = arg;
                }
            }
        }

        private static void ValidateRequiredParams(string[] args, ref Args argsStructure)
        {
            try
            {
                argsStructure.NameOfService = (MapsProvider)Enum.Parse(typeof(MapsProvider), args[1]);
                argsStructure.NameMap = (NameMap)Enum.Parse(typeof(NameMap), args[2]);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid args. Use 'help' or '-h' please.");
            }
        }

        private static void GetImages(ref Args args)
        {
            switch (args.NameMap)
            {
                case NameMap.livonia:
                    ImageRetrieve livonia = new ImageRetrieve(args.NameOfService, args.NameMap, args.TypeMap);
                    livonia.GetImages(args.PathToSave, args.Direction, args.Zoom);
                    break;

                case NameMap.chernorus:
                    throw new NotImplementedException();

                default:
                    break;
            }
        }

        public static bool IsNumeric(string value) => value.All(char.IsNumber);
    }

    public struct Args
    {
        public MapsProvider NameOfService { get; set; }
        public NameMap NameMap { get; set; }
        public string PathToSave { get; set; }
        public TypeMap TypeMap { get; set; }
        public Direction Direction { get; set; }
        public int Zoom { get; set; }
    }
}