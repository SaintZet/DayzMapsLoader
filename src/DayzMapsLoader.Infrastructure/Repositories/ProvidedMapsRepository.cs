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
            => await (GetAll()
                    .IncludeDetails() ?? throw new InvalidOperationException())
                    .ToListAsync();

        public async Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByMapIdAsync(int mapId)
            => await (GetAll()
                    .IncludeDetails() ?? throw new InvalidOperationException())
                    .Where(x => x.Map.Id == mapId)
                    .ToListAsync();

        public async Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByProviderIdAsync(int providerId)
            => await (GetAll()
                    .IncludeDetails() ?? throw new InvalidOperationException())
                    .Where(x => x.MapProvider.Id == providerId)
                    .ToListAsync();

        public async Task<ProvidedMap> GetProvidedMapAsync(int providerId, int mapID, int typeId)
        => (await (GetAll()
                .IncludeDetails() ?? throw new InvalidOperationException())
                .FirstOrDefaultAsync(x => x.MapProvider.Id == providerId && x.Map.Id == mapID && x.MapType.Id == typeId))!;
    }
}