using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Abstractions.Infrastructure
{
    public interface IMapsRepository : IRepository<Map>
    {
        Task<Map> GetProviderByIdAsync(int id);

        Task<IEnumerable<Map>> GetAllMapsAsync();
    }
}