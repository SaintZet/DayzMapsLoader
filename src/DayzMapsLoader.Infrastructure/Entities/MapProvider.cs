using DayzMapsLoader.Infrastructure.Enums;

namespace DayzMapsLoader.Infrastructure.Entities;

public class MapProvider
{
    public int Id { get; set; }
    public MapProviderName Name { get; set; }
    public string? Link { get; set; }
}