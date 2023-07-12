using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;

using DayzMapsLoader.Domain.Contracts.Services;

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
            var fileName = $"{map.MapData.Name}.jpg";
            var zipEntry = zipArchive.CreateEntry(fileName);

            using var originalFileStream = await GetMapInMemoryStreamAsync(map, zoom);
            originalFileStream.Seek(0, SeekOrigin.Begin);

            await using var zipEntryStream = zipEntry.Open();
            await originalFileStream.CopyToAsync(zipEntryStream);
        }

        var archiveData = compressedFileStream.ToArray();
        var archiveName = $"{map.MapProvider.Name}-{map.MapData.Name}-{map.MapType.Name}-{map.Version}-{zoom}.zip";

        return (archiveData, archiveName);
    }

    public async Task<(byte[] data, string name)> DownloadMapImagePartsArchiveAsync(int providerId, int mapID, int typeId, int zoom)
    {
        var map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        var mapParts = await _thirdPartyApiService.GetMapPartsAsync(map, zoom);

        var axisY = mapParts.Weight;
        var axisX = mapParts.Height;

        using MemoryStream compressedFileStream = new();

        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
        {
            for (var y = 0; y < axisY; y++)
            {
                for (var x = 0; x < axisX; x++)
                {
                    var image = mapParts.GetPartOfMap(x, y).Data;
                    var originalFileStream = new MemoryStream(image);

                    var pathToFile = Path.Combine($@"Horizontal_{y}", $"({y}.{x}).png");
                    var zipEntry = zipArchive.CreateEntry(pathToFile);

                    originalFileStream.Seek(0, SeekOrigin.Begin);

                    await using var zipEntryStream = zipEntry.Open();
                    originalFileStream.CopyTo(zipEntryStream);
                }
            }
        }

        var archiveData = compressedFileStream.ToArray();
        var archiveName = $"{map.MapProvider.Name}-{map.MapData.Name}-{map.MapType.Name}-{map.Version}-{zoom}.zip";

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
                var fileName = $"{map.MapData.Name}.jpg";
                var zipEntry = zipArchive.CreateEntry(fileName);

                await using var entryStream = zipEntry.Open();
                var bytes = await GetMapInBytesAsync(map, zoom);
                await entryStream.WriteAsync(bytes);
            }
        }

        var archiveData = compressedFileStream.ToArray();
        var archiveName = $"{maps.First().MapProvider.Name}-{zoom}.zip";

        return (archiveData, archiveName);
    }
}