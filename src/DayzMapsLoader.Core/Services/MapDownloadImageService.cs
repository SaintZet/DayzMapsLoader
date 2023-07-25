using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;

using DayzMapsLoader.Domain.Contracts.Services;

namespace DayzMapsLoader.Core.Services;

internal class MapDownloadImageService : BaseMapDownloadService, IMapDownloadImageService
{
    public MapDownloadImageService(IProvidedMapsRepository providedMapsRepository, IMultipleThirdPartyApiService thirdPartyApiService)
        : base(providedMapsRepository, thirdPartyApiService) { }

    public async Task<byte[]> DownloadMapImageAsync(int providerId, int mapId, int typeId, int zoom)
    {
        var map = await ProvidedMapsRepository.GetProvidedMapAsync(providerId, mapId, typeId).ConfigureAwait(false);

        return await GetMapInBytesAsync(map, zoom);
    }

    public async Task<IEnumerable<byte[]>> DownloadAllMapImagesAsync(int providerId, int zoom)
    {
        var providedMaps = await ProvidedMapsRepository.GetAllProvidedMapsByProviderIdAsync(providerId).ConfigureAwait(false);
        var downloadTasks = providedMaps.Select(async map => await GetMapInBytesAsync(map, zoom).ConfigureAwait(false));

        return await Task.WhenAll(downloadTasks).ConfigureAwait(false);
    }
}