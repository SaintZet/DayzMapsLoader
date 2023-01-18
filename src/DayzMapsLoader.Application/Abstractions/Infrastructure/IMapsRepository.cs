using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Abstractions.Infrastructure
{
    public interface IMapsRepository : IRepository<Map>
    {
        Task<IEnumerable<Map>> GetAllMapsAsync();

        Task<Map> GetMapByIdAsync(int id);
    }
}