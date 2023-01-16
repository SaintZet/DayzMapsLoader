using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Enums;
using DayzMapsLoader.Application.Managers;
using DayzMapsLoader.Application.Types;
using DayzMapsLoader.Domain.Entities;
using System.Drawing.Imaging;

namespace DayzMapsLoader.Application.Services;

public class MapDownloader : IMapDownloader
{
    private readonly IProvidedMapsRepository _providedMapsRepository;

    private readonly ImageMerger _imageMerger = new(new MapSize(256), 25);
    private readonly ProviderManager _providerManager = new();

    public MapDownloader(IProvidedMapsRepository providedMapsRepository)
    {
        _providedMapsRepository = providedMapsRepository;
    }

    public int QualityMultiplier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public async Task<byte[]> DownloadMap(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        var mapParts = _providerManager.GetMapParts(map, zoom);

        Enum.TryParse(map.ImageExtension, true, out ImageExtension extension);

        var bitmap = _imageMerger.Merge(mapParts, extension);

        using (MemoryStream ms = new())
        {
            bitmap.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }
    }

    //public async IAsyncEnumerable<Task<byte[]>> DownloadAllMaps(int providerId, int zoom)
    //{
    //    List<Task<byte[]>> result = new();

    //    var providedMaps = await _providedMapsRepository.GetProvidedMapsByProviderId(providerId).ConfigureAwait(false);

    //    Parallel.ForEach(providedMaps, async map =>
    //    {
    //        var image = await DownloadMap(providerId, map.Id, map.Type.Id, zoom).ConfigureAwait(false);

    //        result.Add(image);
    //    });

    //    return await result.ToListAsync();
    //}

    public async Task<byte[,][]> DownloadMapInParts(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        return _providerManager.GetMapParts(map, zoom).GetRawMapParts();
    }

    //public IEnumerable<MapParts> DownloadAllMapsInParts(int providerId, int zoom)
    //{
    //    List<MapParts> result = new();

    //    Parallel.ForEach(_mapsDbContext.GetProvidedMapsByProviderId(providerId), map =>
    //    {
    //        var image = DownloadMapInParts(providerId, map.Id, map.Type.Id, zoom);

    //        result.Add(image);
    //    });

    //    return result;
    //}
}