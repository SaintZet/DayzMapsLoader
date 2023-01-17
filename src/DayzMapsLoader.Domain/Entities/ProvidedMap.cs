namespace DayzMapsLoader.Domain.Entities;

public class ProvidedMap
{
    public int Id { get; set; }
    public string NameForProvider { get; set; } = string.Empty;
    public virtual MapProvider MapProvider { get; set; } = default!;
    public virtual Map Map { get; set; } = default!;
    public virtual MapType MapType { get; set; } = default!;
    public string MapTypeForProvider { get; set; } = string.Empty;
    public int MaxMapLevel { get; set; }
    public bool IsFirstQuadrant { get; set; }
    public string Version { get; set; } = string.Empty;
    public string ImageExtension { get; set; } = string.Empty;
}