using RequestsHub.Damain.DataTypes;
using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;

namespace RequestsHub.Domain.Maps
{
    internal class Livonia : IMap
    {
        public Livonia(Dictionary<int, MapSize> keyValuePairsSize, List<TypeMap> typesMap, string version)
        {
            KeyValuePairsSize = keyValuePairsSize;
            TypesMap = typesMap;
            Version = version;
        }

        public Livonia()
        {
        }

        public Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
        public NameMap Name => NameMap.livonia;
        public List<TypeMap> TypesMap { get; set; }
        public string Version { get; set; }
    }
}