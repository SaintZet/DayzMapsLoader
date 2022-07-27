using RequestsHub.Domain.Contracts;

namespace RequestsHub.Domain.DataTypes.Maps;

internal abstract class AbstractMap : IMap
{
    public AbstractMap(Dictionary<int, MapSize> keyValuePairsSize, ImageExtension mapExtension, string mapNameForProvider, List<MapType> typesMap, string version, bool isFirstQuadrant = false)
    {
        KeyValuePairsSize = keyValuePairsSize;
        MapExtension = mapExtension;
        MapNameForProvider = mapNameForProvider;
        TypesMap = typesMap;
        Version = version;
        IsFirstQuadrant = isFirstQuadrant;
    }

    public bool IsFirstQuadrant { get; set; }
    public Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
    public ImageExtension MapExtension { get; set; }
    public List<MapType> TypesMap { get; set; }
    public abstract MapName Name { get; }
    public string MapNameForProvider { get; set; }
    public string Version { get; set; }
}