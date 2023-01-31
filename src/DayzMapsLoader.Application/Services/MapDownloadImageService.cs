using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Services;

internal class MapDownloadImageService : BaseMapDownloadService, IMapDownloadImageService
{
    public MapDownloadImageService(IProvidedMapsRepository providedMapsRepository)
        : base(providedMapsRepository) { }

    public async Task<byte[]> DownloadMapImageAsync(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        using MemoryStream memoryStream = GetMapInMemoryStream(map, zoom);

        return memoryStream.ToArray();
    }

    public async Task<byte[,][]> DownloadMapImageInPartsAsync(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        return _externalApiManager.GetMapParts(map, zoom).GetRawMapParts();
    }

    public async Task<IEnumerable<byte[]>> DownloadAllMapImages(int providerId, int zoom)
    {
        List<byte[]> result = new();

        var providedMaps = await _providedMapsRepository.GetAllProvidedMapsByProviderIdAsync(providerId).ConfigureAwait(false);

        Parallel.ForEach(providedMaps, async map =>
        {
            var image = await DownloadMapImageAsync(providerId, map.Id, map.MapType.Id, zoom).ConfigureAwait(false);

            result.Add(image);
        });

        return result;
    }
}