using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Abstractions.Infrastructure
{
    public interface IProvidedMapsRepository : IRepository<ProvidedMap>
    {
        public Task<ProvidedMap> GetProvidedMapAsync(int providerId, int mapID, int typeId);

        public Task<ProvidedMap> GetProvidedMapsByProviderIdAsync(int providerId);

        public Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByProviderIdAsync(int providerId);
    }
}