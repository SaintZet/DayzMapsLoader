using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.MapsProviders;

namespace RequestsHub.Infrastructure;

internal class Args
{
    public Args(string[] args)
    {
        Command = (CommandType)Enum.Parse(typeof(CommandType), args[0]);

        MapProvider nameProvider = (MapProvider)Enum.Parse(typeof(MapProvider), args[1]);
        Provider = SelectProvider(nameProvider);

        if (args.Length > 2 && args.Length < 7)
        {
            ValidateOptionParams(args);
        }
        else
        {
            throw new ArgumentException("Invalid args. Use 'help' or '-h' please.");
        }
    }

    public CommandType Command { get; internal set; }
    public IMapProvider Provider { get; internal set; }
    public MapName NameMap { get; internal set; } = MapName.chernorus;
    public MapType TypeMap { get; internal set; } = MapType.topographic;
    public int Zoom { get; internal set; }
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
        if (Command != CommandType.MergePartsAllMaps && Command != CommandType.GetAllMaps && Command != CommandType.GetAllMapsInParts)
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

            if (Enum.TryParse(arg, out MapType _))
            {
                TypeMap = (MapType)Enum.Parse(typeof(MapType), arg);
                continue;
            }

            if (Directory.Exists(arg) || File.Exists(arg))
            {
                PathToSave = arg;
            }
        }
    }
}