using RequestsHub.Domain.DataTypes;

namespace RequestsHub.Domain.Contracts
{
    internal interface IMap
    {
        Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
        NameMap Name { get; }
        List<TypeMap> TypesMap { get; }
        string Version { get; set; }
    }
}