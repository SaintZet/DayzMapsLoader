using DayzMapsLoader.Domain.Entities.MapProvider;

namespace DayzMapsLoader.Application.Abstractions
{
    public interface IMapsDbContext
    {
        MapProvider GetMapProvider(MapProviderName value);
    }
}