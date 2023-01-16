using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Infrastructure.Repositories
{
    public class MapProvidersRepository : Repository<MapProvider>, IMapProvidersRepository
    {
        public MapProvidersRepository(DayzMapLoaderContext dayzMapLoaderContext)
            : base(dayzMapLoaderContext)
        {
        }

        public async Task<IEnumerable<MapProvider>> GetAllMapProvidersAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<MapProvider> GetProviderByIdAsync(int id)
        {
            return (await GetAll().FirstOrDefaultAsync(x => x.Id == id))!;
        }
    }
}