using DayzMapsLoader.Domain.Abstractions;
using System;

namespace DayzMapsLoader.Domain.Entities;

public sealed class Map : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? LastUpdate { get; set; }
    public string? Author { get; set; }
    public string? Link { get; set; }
}