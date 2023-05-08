using DayzMapsLoader.Application.Abstractions.Infrastructure.Repositories;
using DayzMapsLoader.Application.Abstractions.Infrastructure.Services;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Services;

internal class MapDownloadImageService : BaseMapDownloadService, IMapDownloadImageService
{
    public MapDownloadImageService(IProvidedMapsRepository providedMapsRepository, IMultipleThirdPartyApiService thirdPartyApiService)
        : base(providedMapsRepository, thirdPartyApiService) { }

    public async Task<byte[]> DownloadMapImageAsync(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        using MemoryStream memoryStream = await GetMapInMemoryStreamAsync(map, zoom);

        return memoryStream.ToArray();
    }

    public async Task<byte[,][]> DownloadMapImageInPartsAsync(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        var mapParts = await _thirdPartyApiService.GetMapPartsAsync(map, zoom);

        return mapParts.GetRawMapParts();
    }

    public async Task<IEnumerable<byte[]>> DownloadAllMapImagesAsync(int providerId, int zoom)
    {
        var providedMaps = await _providedMapsRepository.GetAllProvidedMapsByProviderIdAsync(providerId).ConfigureAwait(false);

        var downloadTasks = providedMaps
            .Select(async map => await DownloadMapImageAsync(providerId, map.Id, map.MapType.Id, zoom)
            .ConfigureAwait(false));

        return await Task.WhenAll(downloadTasks).ConfigureAwait(false);
    }
}