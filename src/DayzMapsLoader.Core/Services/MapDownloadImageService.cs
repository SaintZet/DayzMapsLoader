using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Core.Contracts.Services;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Core.Services;

internal class MapDownloadImageService : BaseMapDownloadService, IMapDownloadImageService
{
    public MapDownloadImageService(IProvidedMapsRepository providedMapsRepository, IMultipleThirdPartyApiService thirdPartyApiService)
        : base(providedMapsRepository, thirdPartyApiService) { }

    public async Task<byte[]> DownloadMapImageAsync(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        return await GetMapInBytesAsync(map, zoom);
    }

    public async Task<byte[,][]> DownloadMapImageInPartsAsync(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        var mapParts = await _thirdPartyApiService.GetMapPartsAsync(map, zoom).ConfigureAwait(false); ;

        return mapParts.GetRawMapParts();
    }

    public async Task<IEnumerable<byte[]>> DownloadAllMapImagesAsync(int providerId, int zoom)
    {
        var providedMaps = await _providedMapsRepository.GetAllProvidedMapsByProviderIdAsync(providerId).ConfigureAwait(false);

        var downloadTasks = providedMaps.Select(async map => await GetMapInBytesAsync(map, zoom).ConfigureAwait(false));

        return await Task.WhenAll(downloadTasks).ConfigureAwait(false);
    }
}