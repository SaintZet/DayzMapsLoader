using RequestsHub.Domain.DataTypes;

namespace RequestsHub.Domain.Contracts
{
    internal interface IMap
    {
        Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
        ImageExtension MapExtension { get; set; }
        List<TypeMap> TypesMap { get; }
        MapName MapName { get; }
        string MapNameForProvider { get; set; }
        string Version { get; set; }
    }
}