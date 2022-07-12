using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.MapsProviders;

namespace RequestsHub.Infrastructure;

internal class Args
{
    public Args(string[] args)
    {
        if (args.Length == 2)
        {
            Command = args[0]; //TODO: add validation

            MapProvider nameProvider = (MapProvider)Enum.Parse(typeof(MapProvider), args[1]);
            Provider = SelectProvider(nameProvider);
        }
        else if (args.Length > 2 && args.Length < 7)
        {
            ValidateOptionParams(args);
        }
        else
        {
            throw new ArgumentException("Invalid args. Use 'help' or '-h' please.");
        }
    }

    public string Command { get; internal set; }
    public IMapProvider Provider { get; internal set; }
    public MapName NameMap { get; internal set; } = MapName.chernorus;
    public TypeMap TypeMap { get; internal set; } = TypeMap.topographic;
    public int Zoom { get; internal set; } = 0;
    public string PathToSave { get; internal set; } = string.Empty;

    private IMapProvider SelectProvider(MapProvider nameProvider)
    {
        switch (nameProvider)
        {
            case MapProvider.xam:
                return new Xam();

            case MapProvider.ginfo:
                return new Ginfo();

            default:
                throw new ArgumentException("Invalid args. Use 'help' or '-h' please.");
        }
    }

    private void ValidateOptionParams(string[] args)
    {
        if (Command != "MergePartsAllMap" && Command != "GetAllMaps" && Command != "GetAllMapsInParts")
        {
            NameMap = (MapName)Enum.Parse(typeof(MapName), args[2]);
        }

        foreach (string arg in args)
        {
            if (arg.Contains("-zoom:"))
            {
                string argNew = arg.Replace("-zoom:", "");
                if (argNew.All(char.IsNumber))
                {
                    Zoom = int.Parse(argNew);
                    continue;
                }
            }

            if (Enum.TryParse(arg, out TypeMap _))
            {
                TypeMap = (TypeMap)Enum.Parse(typeof(TypeMap), arg);
                continue;
            }

            if (Directory.Exists(arg) || File.Exists(arg))
            {
                PathToSave = arg;
            }
        }
    }
}