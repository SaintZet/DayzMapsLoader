using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Abstractions.Infrastructure
{
    public interface IProvidedMapsRepository : IRepository<ProvidedMap>
    {
        public Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsAsync();

        public Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByProviderIdAsync(int providerId);

        public Task<ProvidedMap> GetProvidedMapAsync(int providerId, int mapID, int typeId);

        public Task<ProvidedMap> GetProvidedMapsByProviderIdAsync(int providerId);
    }
}