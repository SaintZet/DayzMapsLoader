﻿using DayzMapsLoader.Domain.Abstractions;

namespace DayzMapsLoader.Domain.Entities;

public sealed class ProvidedMap : IEntity
{
    public int Id { get; set; }
    public string NameForProvider { get; set; } = string.Empty;
    public MapProvider MapProvider { get; set; } = default!;
    public Map Map { get; set; } = default!;
    public MapType MapType { get; set; } = default!;
    public string MapTypeForProvider { get; set; } = string.Empty;
    public int MaxMapLevel { get; set; }
    public bool IsFirstQuadrant { get; set; }
    public string Version { get; set; } = string.Empty;
    public string ImageExtension { get; set; } = string.Empty;
}