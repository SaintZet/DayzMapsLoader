using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.DataTypes.Maps
{
    internal class Esseker : IMap
    {
        public Esseker(Dictionary<int, MapSize> keyValuePairsSize, List<TypeMap> typesMap, string version)
        {
            KeyValuePairsSize = keyValuePairsSize;
            TypesMap = typesMap;
            Version = version;
        }

        public Esseker()
        {
        }

        public Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
        public MapName MapName => MapName.esseker;
        public List<TypeMap> TypesMap { get; set; }
        public string Version { get; set; }
        public string MapNameForProvider { get; set; }
        public ImageExtension MapExtension { get; set; }
        public bool IsFirstQuadrant { get; set; }
    }
}