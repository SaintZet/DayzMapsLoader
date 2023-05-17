using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;

internal interface IMapsRepository : IBaseRepository<Map>
{
    Task<IEnumerable<Map>> GetAllMapsAsync();

    Task<Map> GetMapByIdAsync(int id);
}