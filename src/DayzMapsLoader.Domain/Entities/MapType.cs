using DayzMapsLoader.Domain.Abstractions;

namespace DayzMapsLoader.Domain.Entities;

public class MapType : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}