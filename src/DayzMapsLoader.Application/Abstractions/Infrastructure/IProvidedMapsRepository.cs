using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Abstractions.Infrastructure;

public interface IProvidedMapsRepository : IBaseRepository<ProvidedMap>
{
    public Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsAsync();

    public Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByProviderIdAsync(int providerId);

    public Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByMapIdAsync(int mapId);

    public Task<ProvidedMap> GetProvidedMapAsync(int providerId, int mapID, int typeId);
}