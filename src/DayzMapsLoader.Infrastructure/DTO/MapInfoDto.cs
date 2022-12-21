namespace DayzMapsLoader.Infrastructure.DTO;

public class MapInfoDto
{
    public int Name { get; set; }
    public string? NameForProvider { get; set; }
    public int ZoomLevelRatioToSize { get; set; }
    public int MapExtension { get; set; }
    public List<int>? TypesMap { get; set; }
    public string? Version { get; set; }
    public bool IsFirstQuadrant { get; set; }
}