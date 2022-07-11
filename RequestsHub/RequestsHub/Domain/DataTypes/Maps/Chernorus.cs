using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.DataTypes.Maps
{
    internal class Chernorus : IMap
    {
        public Chernorus()
        {
        }

        public Chernorus(Dictionary<int, MapSize> keyValuePairsSize, List<TypeMap> typesMap, ImageExtension extension, string version)
        {
            KeyValuePairsSize = keyValuePairsSize;
            TypesMap = typesMap;
            MapExtension = extension;
            Version = version;
        }

        public Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
        public MapName MapName => MapName.chernorus;
        public List<TypeMap> TypesMap { get; set; }
        public string Version { get; set; }
        public string MapNameForProvider { get; set; }
        public ImageExtension MapExtension { get; set; }
        public bool IsFirstQuadrant { get; set; }
    }
}