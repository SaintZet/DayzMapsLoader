using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Core.Contracts.Services;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Shared.Constants;
using DayzMapsLoader.Shared.Enums;
using DayzMapsLoader.Shared.Wrappers;
using System.Drawing.Imaging;

namespace DayzMapsLoader.Core.Services;

internal abstract class BaseMapDownloadService
{
    protected readonly IProvidedMapsRepository _providedMapsRepository;
    protected readonly IMultipleThirdPartyApiService _thirdPartyApiService;
    protected readonly IMapMergeService _mapMergeService;

    public BaseMapDownloadService(IProvidedMapsRepository providedMapsRepository, IMultipleThirdPartyApiService thirdPartyApiService)
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
        using MemoryStream memoryStream = await GetMergedMapMemoryStream(map, zoom);

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