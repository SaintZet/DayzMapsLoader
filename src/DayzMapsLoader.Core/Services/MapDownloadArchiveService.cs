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
        var map = await ProvidedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        using MemoryStream compressedFileStream = new();

        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
        {
            var fileName = $"{map.Map.Name}.jpg";
            var zipEntry = zipArchive.CreateEntry(fileName);

            using var originalFileStream = await GetMapInMemoryStreamAsync(map, zoom);
            originalFileStream.Seek(0, SeekOrigin.Begin);

            await using var zipEntryStream = zipEntry.Open();
            await originalFileStream.CopyToAsync(zipEntryStream);
        }

        var archiveData = compressedFileStream.ToArray();
        var archiveName = $"{map.MapProvider.Name}-{map.Map.Name}-{map.MapType.Name}-{map.Version}-{zoom}.zip";

        return (archiveData, archiveName);
    }

    public async Task<(byte[] data, string name)> DownloadMapImagePartsArchiveAsync(int providerId, int mapID, int typeId, int zoom)
    {
        var map = await ProvidedMapsRepository.GetProvidedMapAsync(providerId, mapID, typeId).ConfigureAwait(false);

        var partsEnumerable = ThirdPartyApiService.GetMapPartsAsync(map, zoom).ConfigureAwait(false);
        var enumerator = partsEnumerable.GetAsyncEnumerator();
        using MemoryStream compressedFileStream = new();
        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
        {
	        while (await enumerator.MoveNextAsync())
	        {
		        var part = enumerator.Current;
		        var originalFileStream = new MemoryStream(part.Data);

		        var pathToFile = Path.Combine($@"Horizontal_{part.Y}", $"({part.Y}.{part.X}).png");
		        var zipEntry = zipArchive.CreateEntry(pathToFile);

		        originalFileStream.Seek(0, SeekOrigin.Begin);

		        await using var zipEntryStream = zipEntry.Open();
		        await originalFileStream.CopyToAsync(zipEntryStream);
	        }
        }

        var archiveData = compressedFileStream.ToArray();
        var archiveName = $"{map.MapProvider.Name}-{map.Map.Name}-{map.MapType.Name}-{map.Version}-{zoom}.zip";

        return (archiveData, archiveName);
    }

    public async Task<(byte[] data, string name)> DownloadAllMapsImagesArchiveAsync(int providerId, int zoom)
    {
        var maps = await ProvidedMapsRepository.GetAllProvidedMapsByProviderIdAsync(providerId).ConfigureAwait(false);

        using var compressedFileStream = new MemoryStream();
        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
        {
            foreach (var map in maps)
            {
                var fileName = $"{map.Map.Name}.jpg";
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