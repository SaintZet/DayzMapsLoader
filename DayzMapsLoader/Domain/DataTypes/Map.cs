using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.DataTypes;

internal class Map : IMap
{
    public Map(MapName name, Dictionary<int, MapSize> keyValuePairsSize, ImageExtension mapExtension, string mapNameForProvider, List<MapType> typesMap, string version, bool isFirstQuadrant = false)
    {
        Name = name;
        ZoomLevelRatioToSize = keyValuePairsSize;
        MapExtension = mapExtension;
        MapNameForProvider = mapNameForProvider;
        TypesMap = typesMap;
        Version = version;
        IsFirstQuadrant = isFirstQuadrant;
    }

    public bool IsFirstQuadrant { get; set; }
    public Dictionary<int, MapSize> ZoomLevelRatioToSize { get; set; }
    public ImageExtension MapExtension { get; set; }
    public List<MapType> TypesMap { get; set; }
    public MapName Name { get; }
    public string MapNameForProvider { get; set; }
    public string Version { get; set; }
}