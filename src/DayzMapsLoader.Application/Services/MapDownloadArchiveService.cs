using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Domain.Entities;
using System.IO.Compression;

namespace DayzMapsLoader.Application.Services;

internal class MapDownloadArchiveService : BaseMapDownloadService, IMapDownloadArchiveService
{
    public MapDownloadArchiveService(IProvidedMapsRepository providedMapsRepository)
        : base(providedMapsRepository) { }

    public async Task<(byte[] data, string name)> DownloadMapImageArchiveAsync(int providerId, int mapID, int typeId, int zoom)
    {
        var map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        using MemoryStream compressedFileStream = new();

        string fileName = $"{map.Map.Name}.jpg";

        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
        {
            ZipArchiveEntry zipEntry = zipArchive.CreateEntry(fileName);

            using MemoryStream originalFileStream = GetMapInMemoryStream(map, zoom);
            originalFileStream.Seek(0, SeekOrigin.Begin);

            using Stream zipEntryStream = zipEntry.Open();
            originalFileStream.CopyTo(zipEntryStream);
        }

        byte[] archiveData = compressedFileStream.ToArray();
        string archiveName = $"{map.MapProvider.Name}-{map.Map.Name}-{map.MapType.Name}-{map.Version}-{zoom}.zip";

        return (archiveData, archiveName);
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

        byte[] archiveData = compressedFileStream.ToArray();
        string archiveName = $"{map.MapProvider.Name}-{map.Map.Name}-{map.MapType.Name}-{map.Version}-{zoom}.zip";

        return (archiveData, archiveName);
    }

    public async Task<(byte[] data, string name)> DownloadAllMapsImagesArchiveAsync(int providerId, int zoom)
    {
        var maps = await _providedMapsRepository.GetAllProvidedMapsByProviderIdAsync(providerId).ConfigureAwait(false);

        using MemoryStream compressedFileStream = new();

        foreach (var map in maps)
        {
            using var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false);

            string fileName = $"{map.Map.Name}.jpg";
            ZipArchiveEntry zipEntry = zipArchive.CreateEntry($"{map.Map.Name}.jpg");

            using MemoryStream originalFileStream = GetMapInMemoryStream(map, zoom);
            originalFileStream.Seek(0, SeekOrigin.Begin);

            using Stream zipEntryStream = zipEntry.Open();
            originalFileStream.CopyTo(zipEntryStream);
        }

        byte[] archiveData = compressedFileStream.ToArray();
        string archiveName = $"{maps.First().MapProvider.Name}-{zoom}.zip";

        return (archiveData, archiveName);
    }
}