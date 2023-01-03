using DayzMapsLoader.Domain.Entities.MapProvider;

namespace DayzMapsLoader.Application.Abstractions.Infrastructure;

public interface IMapsDbContext
{
    MapProvider GetMapProvider(MapProviderName value);
    IList<MapProvider> GetMapProviders();
}