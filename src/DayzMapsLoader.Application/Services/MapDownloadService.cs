using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Enums;
using DayzMapsLoader.Application.Managers;
using DayzMapsLoader.Application.Types;
using DayzMapsLoader.Domain.Entities;
using System.Drawing.Imaging;
using System.IO.Compression;

namespace DayzMapsLoader.Application.Services;

public class MapDownloadService : IMapDownloadService
{
    private readonly IProvidedMapsRepository _providedMapsRepository;
    private readonly ImageMerger _imageMerger = new(new MapSize(256), 25);
    private readonly ExternalApiCaller _externalApiCaller = new();

    public MapDownloadService(IProvidedMapsRepository providedMapsRepository)
    {
        _providedMapsRepository = providedMapsRepository;
    }

    public int QualityMultiplier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public async Task<byte[]> DownloadMapImage(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        using MemoryStream memoryStream = GetMapInMemoryStream(map, zoom);

        return memoryStream.ToArray();
    }

    public async Task<byte[,][]> DownloadMapImageInParts(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        return _externalApiCaller.GetMapParts(map, zoom).GetRawMapParts();
    }

    public async Task<(byte[] data, string name)> DownloadMapImageArchive(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        using MemoryStream compressedFileStream = new();

        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
        {
            ZipArchiveEntry zipEntry = zipArchive.CreateEntry($"{map.Map.Name}.jpg");

            using MemoryStream originalFileStream = GetMapInMemoryStream(map, zoom);
            originalFileStream.Seek(0, SeekOrigin.Begin);

            using Stream zipEntryStream = zipEntry.Open();
            originalFileStream.CopyTo(zipEntryStream);
        }

        return (compressedFileStream.ToArray(), $"{map.MapProvider.Name}-{map.Map.Name}-{map.MapType.Name}-{map.Version}-{zoom}.zip");
    }

    public async Task<(byte[] data, string name)> DownloadMapImagePartsArchive(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        var mapParts = _externalApiCaller.GetMapParts(map, zoom);

        int axisY = mapParts.Weight;
        int axisX = mapParts.Height;

        string pathToFile;

        using MemoryStream compressedFileStream = new();

        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
        {
            for (int y = 0; y < axisY; y++)
            {
                for (int x = 0; x < axisX; x++)
                {
                    var image = mapParts.GetPartOfMap(x, y).Data;

                    var originalFileStream = new MemoryStream(image);

                    pathToFile = Path.Combine($@"Horizontal_{y}", $"({x}.{y}).png");

                    ZipArchiveEntry zipEntry = zipArchive.CreateEntry(pathToFile);

                    originalFileStream.Seek(0, SeekOrigin.Begin);

                    using Stream zipEntryStream = zipEntry.Open();
                    originalFileStream.CopyTo(zipEntryStream);
                }
            }
        }

        return (compressedFileStream.ToArray(), $"{map.MapProvider.Name}-{map.Map.Name}-{map.MapType.Name}-{map.Version}-{zoom}.zip");
    }

    private MemoryStream GetMapInMemoryStream(ProvidedMap map, int zoom)
    {
        var mapParts = _externalApiCaller.GetMapParts(map, zoom);

        Enum.TryParse(map.ImageExtension, true, out ImageExtension extension);

        var image = _imageMerger.Merge(mapParts, extension);

        var memoryStream = new MemoryStream();

        image.Save(memoryStream, ImageFormat.Png);

        return memoryStream;
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