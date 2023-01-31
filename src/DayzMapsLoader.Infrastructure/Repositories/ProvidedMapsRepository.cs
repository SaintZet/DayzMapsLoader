using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Infrastructure.Repositories
{
    public class ProvidedMapsRepository : Repository<ProvidedMap>, IProvidedMapsRepository
    {
        public ProvidedMapsRepository(DayzMapLoaderContext dayzMapLoaderContext)
            : base(dayzMapLoaderContext)
        {
        }

        public async Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsAsync()
            => await GetAll()
            .Include(p => p.MapProvider)
            .Include(p => p.Map)
            .Include(p => p.MapType)
            .ToListAsync();

        public async Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByMapIdAsync(int mapId)
            => await GetAll()
            .Include(p => p.MapProvider)
            .Include(p => p.Map)
            .Include(p => p.MapType)
            .Where(x => x.Map.Id == mapId)
            .ToListAsync();

        public async Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByProviderIdAsync(int providerId)
            => await GetAll()
            .Include(p => p.MapProvider)
            .Include(p => p.Map)
            .Include(p => p.MapType)
            .Where(x => x.MapProvider.Id == providerId)
            .ToListAsync();

        public async Task<ProvidedMap> GetProvidedMapAsync(int providerId, int mapID, int typeId)
        => (await GetAll()
            .Include(p => p.MapProvider)
            .Include(p => p.Map)
            .Include(p => p.MapType)
            .FirstOrDefaultAsync(x => x.MapProvider.Id == providerId && x.Map.Id == mapID && x.MapType.Id == typeId))!;
    }
}