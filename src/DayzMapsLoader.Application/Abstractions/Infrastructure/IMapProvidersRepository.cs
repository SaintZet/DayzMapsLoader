using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Abstractions.Infrastructure
{
    public interface IMapProvidersRepository : IRepository<MapProvider>
    {
        Task<MapProvider> GetProviderByIdAsync(int id);

        Task<IEnumerable<MapProvider>> GetAllMapProvidersAsync();
    }
}