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

        public async Task<IEnumerable<ProvidedMap>> GetAllProvidedMapsByProviderIdAsync(int providerId)
        {
            return await GetAll().Where(x => x.MapProvider.Id == providerId).ToListAsync();
        }

        public async Task<ProvidedMap> GetProvidedMapAsync(int providerId, int mapID, int typeId)
        {
            var x = await GetAll().ToListAsync();
            return (await GetAll().FirstOrDefaultAsync(x =>
            x.MapProvider.Id == providerId &&
            x.Map.Id == mapID &&
            x.MapType.Id == typeId))!;
        }

        public async Task<ProvidedMap> GetProvidedMapsByProviderIdAsync(int id)
        {
            return (await GetAll().FirstOrDefaultAsync(x => x.MapProvider.Id == id))!;
        }
    }
}