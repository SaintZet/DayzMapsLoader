using RequestsHub.Damain.DataTypes;
using RequestsHub.Domain.DataTypes;

namespace RequestsHub.Domain.Contracts
{
    internal interface IMap
    {
        Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
        NameMap Name { get; }
        List<TypeMap> TypesMap { get; set; }
        string Version { get; set; }
    }
}