using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.DataTypes.Maps
{
    internal class Chernorus : IMap
    {
        public Chernorus()
        {
        }

        public Chernorus(Dictionary<int, MapSize> keyValuePairsSize, List<TypeMap> typesMap, string version)
        {
            KeyValuePairsSize = keyValuePairsSize;
            TypesMap = typesMap;
            Version = version;
        }

        public Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
        public NameMap Name => NameMap.chernorus;
        public List<TypeMap> TypesMap { get; set; }
        public string Version { get; set; }
    }
}