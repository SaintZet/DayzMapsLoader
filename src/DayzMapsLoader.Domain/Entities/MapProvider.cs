using DayzMapsLoader.Domain.Abstractions;

namespace DayzMapsLoader.Domain.Entities;

public class MapProvider : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UrlQueryTemplate { get; set; } = string.Empty;
    public string? Link { get; set; }
}