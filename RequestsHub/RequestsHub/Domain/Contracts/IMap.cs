namespace RequestsHub.Domain.Contracts;

internal interface IMap
{
    Dictionary<int, MapSize> KeyValuePairsSize { get; set; }
    ImageExtension MapExtension { get; set; }
    List<MapType> TypesMap { get; }
    MapName Name { get; }
    string MapNameForProvider { get; set; }
    string Version { get; set; }
    bool IsFirstQuadrant { get; }
}