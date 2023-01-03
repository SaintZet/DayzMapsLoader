namespace DayzMapsLoader.Domain.Entities.Map;

public record struct MapInfo
{
    public bool IsFirstQuadrant { get; set; }
    public IDictionary<int, Size> ZoomLevelRatioSize { get; set; }
    public IEnumerable<MapType> TypesMap { get; set; }
    public ImageExtension MapExtension { get; set; }
    public MapName Name { get; set; }
    public string NameForProvider { get; set; }
    public string Version { get; set; }
}