using DayzMapsLoader.Core.Builders;
using DayzMapsLoader.Core.Contracts.Builders;
using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Core.Constants;
using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Models;

using DayzMapsLoader.Domain.Entities;

using System.Drawing.Imaging;

namespace DayzMapsLoader.Core.Services;

internal abstract class BaseMapDownloadService
{
    protected readonly IProvidedMapsRepository ProvidedMapsRepository;
    protected readonly IMultipleThirdPartyApiService ThirdPartyApiService;
    
    protected BaseMapDownloadService(IProvidedMapsRepository providedMapsRepository, IMultipleThirdPartyApiService thirdPartyApiService)
    {
        ProvidedMapsRepository = providedMapsRepository;
        ThirdPartyApiService = thirdPartyApiService;
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
	    var partsEnumerable = ThirdPartyApiService.GetMapPartsAsync(map, zoom).ConfigureAwait(false);
	    var enumerator = partsEnumerable.GetAsyncEnumerator();
	    var mapSize = new MapSize(MapImageConstants.ImageWidthPixels, MapImageConstants.ImageHeightPixels);
	    Enum.TryParse(map.ImageExtension, true, out ImageExtension extension);
	    
	    await using var builder = new MapBuilder(mapSize, zoom, MapImageConstants.ImageSizeImprovementPercent);

	    while (await enumerator.MoveNextAsync())
	    {
		    builder.Append(enumerator.Current, extension);
	    }


        var image = builder.Build();

        var memoryStream = new MemoryStream();

        image.Save(memoryStream, ImageFormat.Png);

        return memoryStream;
    }
}