using DayzMapsLoader.Domain.Entities.Map;

namespace DayzMapsLoader.Domain.Entities.MapProvider;

public record struct MapProvider(MapProviderName Name, IEnumerable<MapInfo> Maps);