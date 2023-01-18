using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Abstractions.Infrastructure
{
    public interface IMapProvidersRepository : IRepository<MapProvider>
    {
        Task<IEnumerable<MapProvider>> GetAllMapProvidersAsync();

        Task<MapProvider> GetProviderByIdAsync(int id);
    }
}