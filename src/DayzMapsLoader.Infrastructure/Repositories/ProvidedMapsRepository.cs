using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Infrastructure.Repositories
{
    public class ProvidedMapsRepository : BaseRepository<ProvidedMap>, IProvidedMapsRepository
    {
        public ProvidedMapsRepository(DayzMapLoaderContext dayzMapLoaderContext)
            : base(dayzMapLoaderContext) { }

        public async Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsAsync()
            => await GetAll()
            .IncludeDetails(true)
            .ToListAsync();

        public async Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByMapIdAsync(int mapId)
            => await GetAll()
            .IncludeDetails(true)
            .Where(x => x.Map.Id == mapId)
            .ToListAsync();

        public async Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByProviderIdAsync(int providerId)
            => await GetAll()
            .IncludeDetails(true)
            .Where(x => x.MapProvider.Id == providerId)
            .ToListAsync();

        public async Task<ProvidedMap> GetProvidedMapAsync(int providerId, int mapID, int typeId)
        => (await GetAll()
            .IncludeDetails(true)
            .FirstOrDefaultAsync(x => x.MapProvider.Id == providerId && x.Map.Id == mapID && x.MapType.Id == typeId))!;
    }
}