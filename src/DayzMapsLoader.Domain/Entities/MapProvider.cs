namespace DayzMapsLoader.Domain.Entities;

public class MapProvider
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Link { get; set; }
}