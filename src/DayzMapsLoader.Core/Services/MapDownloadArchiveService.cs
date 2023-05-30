using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Core.Contracts.Services;
using DayzMapsLoader.Domain.Entities;

using System.IO.Compression;

namespace DayzMapsLoader.Core.Services;

internal class MapDownloadArchiveService : BaseMapDownloadService, IMapDownloadArchiveService
{
    public MapDownloadArchiveService(IProvidedMapsRepository providedMapsRepository, IMultipleThirdPartyApiService thirdPartyApiService)
        : base(providedMapsRepository, thirdPartyApiService) { }

    public async Task<(byte[] data, string name)> DownloadMapImageArchiveAsync(int providerId, int mapID, int typeId, int zoom)
    {
        var map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        using MemoryStream compressedFileStream = new();

        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
        {
            string fileName = $"{map.Map.Name}.jpg";
            ZipArchiveEntry zipEntry = zipArchive.CreateEntry(fileName);

            using MemoryStream originalFileStream = await GetMapInMemoryStreamAsync(map, zoom);
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

        var mapParts = await _thirdPartyApiService.GetMapPartsAsync(map, zoom);

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

                    pathToFile = Path.Combine($@"Horizontal_{y}", $"({y}.{x}).png");

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

        using var compressedFileStream = new MemoryStream();
        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
        {
            foreach (var map in maps)
            {
                var fileName = $"{map.Map.Name}.jpg";
                var zipEntry = zipArchive.CreateEntry(fileName);

                using var entryStream = zipEntry.Open();
                var bytes = await GetMapInBytesAsync(map, zoom);
                entryStream.Write(bytes, 0, bytes.Length);
            }
        }

        var archiveData = compressedFileStream.ToArray();
        var archiveName = $"{maps.First().MapProvider.Name}-{zoom}.zip";

        return (archiveData, archiveName);
    }
}