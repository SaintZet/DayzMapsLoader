using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Infrastructure.Repositories
{
    public class MapsRepository : Repository<Map>, IMapsRepository
    {
        public MapsRepository(DayzMapLoaderContext dayzMapLoaderContext)
            : base(dayzMapLoaderContext)
        {
        }

        public async Task<IEnumerable<Map>> GetAllMapsAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Map> GetProviderByIdAsync(int id)
        {
            return (await GetAll().FirstOrDefaultAsync(x => x.Id == id))!;
        }
    }
}