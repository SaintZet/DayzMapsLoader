using DayzMapsLoader.Domain.Common;
using DayzMapsLoader.Domain.Map;
using DayzMapsLoader.Donain.Map;
using System.Net;

namespace DayzMapsLoader.Domain.MapProvider;

public record struct MapProvider(MapProviderName Name, List<MapInfo> Maps);