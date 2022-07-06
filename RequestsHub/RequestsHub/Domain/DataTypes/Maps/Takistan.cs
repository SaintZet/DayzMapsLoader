using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.DataTypes.Maps
{
    internal class Takistan : IMap
    {
        public Takistan(Dictionary<int, MapSize> keyValuePairsSize, List<TypeMap> typesMap, string version)
        {
            KeyValuePairsSize = keyValuePairsSize;
            TypesMap = typesMap;
            Version = version;
        }

        public Takistan()
        {
        }

        public Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
        public MapName MapName => MapName.takistan;
        public List<TypeMap> TypesMap { get; set; }
        public string Version { get; set; }
        public string MapNameForProvider { get; set; }
        public ImageExtension MapExtension { get; set; }
    }
}