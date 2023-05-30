using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Core.Contracts.Services;
using DayzMapsLoader.Core.Constants;
using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Models;

using DayzMapsLoader.Domain.Entities;

using System.Drawing.Imaging;

namespace DayzMapsLoader.Core.Services;

internal abstract class BaseMapDownloadService
{
    protected readonly IProvidedMapsRepository _providedMapsRepository;
    protected readonly IMultipleThirdPartyApiService _thirdPartyApiService;
    private readonly IMapMergeService _mapMergeService;

    protected BaseMapDownloadService(IProvidedMapsRepository providedMapsRepository, IMultipleThirdPartyApiService thirdPartyApiService)
    {
        _providedMapsRepository = providedMapsRepository;
        _thirdPartyApiService = thirdPartyApiService;

        var mapSize = new MapSize(MapImageConstants.ImageWidthPixels, MapImageConstants.ImageHeightPixels);
        _mapMergeService = new MapMergeService(mapSize, MapImageConstants.ImageSizeImprovementPercent);
    }

    protected async Task<MemoryStream> GetMapInMemoryStreamAsync(ProvidedMap map, int zoom)
        => await GetMergedMapMemoryStream(map, zoom);

    protected async Task<byte[]> GetMapInBytesAsync(ProvidedMap map, int zoom)
    {
        using var memoryStream = await GetMergedMapMemoryStream(map, zoom);

        return memoryStream.ToArray();
    }

    private async Task<MemoryStream> GetMergedMapMemoryStream(ProvidedMap map, int zoom)
    {
        var mapParts = await _thirdPartyApiService.GetMapPartsAsync(map, zoom);

        Enum.TryParse(map.ImageExtension, true, out ImageExtension extension);

        var image = _mapMergeService.Merge(mapParts, extension);

        var memoryStream = new MemoryStream();

        image.Save(memoryStream, ImageFormat.Png);

        return memoryStream;
    }
}