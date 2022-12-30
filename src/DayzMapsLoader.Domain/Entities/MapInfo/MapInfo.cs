namespace DayzMapsLoader.Domain.Entities.Map;

public record struct MapInfo
{
    public bool IsFirstQuadrant { get; set; }
    public Dictionary<int, MapSize> ZoomLevelRatioToSize { get; set; }
    public List<MapType> TypesMap { get; set; }
    public ImageExtension MapExtension { get; set; }
    public MapName Name { get; set; }
    public string NameForProvider { get; set; }
    public string Version { get; set; }
}