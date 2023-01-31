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
    private readonly ImageMergeManager _imageMergeManager;
    private readonly ExternalApiManager _externalApiManager;

    private int _qualityMultiplier = 25;

    public MapDownloadService(IProvidedMapsRepository providedMapsRepository)
    {
        _providedMapsRepository = providedMapsRepository;
        _imageMergeManager = new ImageMergeManager(new MapSize(256), _qualityMultiplier);
        _externalApiManager = new ExternalApiManager();
    }

    public int QualityMultiplier
    {
        get => _qualityMultiplier;
        set
        {
            _qualityMultiplier = value;
            _imageMergeManager.DpiImprovementPercent = value;
        }
    }

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

    public async Task<(byte[] data, string name)> DownloadMapImageArchiveAsync(int providerId, int mapID, int typeId, int zoom)
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

    public async Task<(byte[] data, string name)> DownloadMapImagePartsArchiveAsync(int providerId, int mapID, int typeId, int zoom)
    {
        ProvidedMap map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        var mapParts = _externalApiManager.GetMapParts(map, zoom);

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

    public async Task<(byte[] data, string name)> DownloadAllMapsImagesArchiveAsync(int providerId, int zoom)
    {
        var maps = await _providedMapsRepository.GetAllProvidedMapsByProviderIdAsync(providerId).ConfigureAwait(false);

        using MemoryStream compressedFileStream = new();

        foreach (var map in maps)
        {
            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
            {
                ZipArchiveEntry zipEntry = zipArchive.CreateEntry($"{map.Map.Name}.jpg");

                using MemoryStream originalFileStream = GetMapInMemoryStream(map, zoom);
                originalFileStream.Seek(0, SeekOrigin.Begin);

                using Stream zipEntryStream = zipEntry.Open();
                originalFileStream.CopyTo(zipEntryStream);
            }
        }

        return (compressedFileStream.ToArray(), $"{maps.First().MapProvider.Name}-{zoom}.zip");
    }

    private MemoryStream GetMapInMemoryStream(ProvidedMap map, int zoom)
    {
        var mapParts = _externalApiManager.GetMapParts(map, zoom);

        Enum.TryParse(map.ImageExtension, true, out ImageExtension extension);

        var image = _imageMergeManager.Merge(mapParts, extension);

        var memoryStream = new MemoryStream();

        image.Save(memoryStream, ImageFormat.Png);

        return memoryStream;
    }
}