using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;

public interface IMapProvidersRepository : IBaseRepository<MapProvider>
{
    Task<IEnumerable<MapProvider>> GetAllMapProvidersAsync();

    Task<MapProvider> GetProviderByIdAsync(int id);
}