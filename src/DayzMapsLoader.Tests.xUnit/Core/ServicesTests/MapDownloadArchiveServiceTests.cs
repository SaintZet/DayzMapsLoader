using DayzMapsLoader.Domain.Contracts.Services;

using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Core.Models;
using DayzMapsLoader.Tests.xUnit.Core.TestData.MapDownload;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace DayzMapsLoader.Tests.xUnit.Core.ServicesTests;

public class MapDownloadArchiveServiceTests
{
    private readonly IMapDownloadArchiveService _downloadArchiveService;
    private readonly IMediator _mediator;

    public MapDownloadArchiveServiceTests()
    {
        var serviceProvider = new ServiceCollection().BuildCollection();

        _downloadArchiveService = serviceProvider.GetRequiredService<IMapDownloadArchiveService>();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    [Theory]
    [Trait("Category", "Integration")]
    [MemberData(nameof(DataGenerator.YieldReturnAllStages), MemberType = typeof(DataGenerator))]
    public async Task DownloadMapImageArchiveAsync_ShouldReturnZipArchive(int providerId, int mapId, int typeId, int zoom)
    {
        // Arrange
        var query = new GetProvidedMapsQueryByDetailsQuery(providerId, mapId, typeId);
        var map = await _mediator.Send(query).ConfigureAwait(false);
        var expectedArchiveName = $"{map.MapProvider.Name}-{map.Map.Name}-{map.MapType.Name}-{map.Version}-{zoom}.zip";

        // Act
        var (archiveData, archiveName) = await _downloadArchiveService.DownloadMapImageArchiveAsync(providerId, mapId, typeId, zoom);

        // Assert
        Assert.Equal(expectedArchiveName, archiveName);

        using var stream = new MemoryStream(archiveData);
        using var archive = new ZipArchive(stream);
        Assert.Single(archive.Entries);

        var entry = archive.Entries[0];
        Assert.Equal($"{map.Map.Name}.jpg", entry.Name);

        await using var entryStream = entry.Open();
        using var memoryStream = new MemoryStream();
        await entryStream.CopyToAsync(memoryStream);
        var imageData = memoryStream.ToArray();
        Assert.NotEmpty(imageData);
    }

    [Theory]
    [Trait("Category", "Integration")]
    [MemberData(nameof(DataGenerator.YieldReturnAllStages), MemberType = typeof(DataGenerator))]
    public async Task DownloadMapImagePartsArchiveAsync_ShouldReturnZipArchive(int providerId, int mapId, int typeId, int zoomLevel)
    {
        // Arrange
        var query = new GetProvidedMapsQueryByDetailsQuery(providerId, mapId, typeId);
        var map = await _mediator.Send(query).ConfigureAwait(false);

        var expectedArchiveName = $"{map.MapProvider.Name}-{map.Map.Name}-{map.MapType.Name}-{map.Version}-{zoomLevel}.zip";
        var mapSize = MapSize.ConvertZoomLevelRatioSize(zoomLevel);

        // Act
        var (archiveData, archiveName) = await _downloadArchiveService.DownloadMapImagePartsArchiveAsync(providerId, mapId, typeId, zoomLevel);

        // Assert
        Assert.Equal(expectedArchiveName, archiveName);

        using var stream = new MemoryStream(archiveData);
        using var archive = new ZipArchive(stream);
        Assert.Equal(mapSize.Height * mapSize.Width, archive.Entries.Count); // assert that all map parts are present in the archive

        foreach (var entry in archive.Entries)
        {
            Assert.StartsWith("Horizontal_", entry.FullName); // assert that the entry is located in the correct folder

            await using var entryStream = entry.Open();
            using var memoryStream = new MemoryStream();
            await entryStream.CopyToAsync(memoryStream);
            var imageData = memoryStream.ToArray();
            Assert.NotEmpty(imageData); // assert that the image data is not empty
        }
    }

    [Theory]
    [Trait("Category", "Integration")]
    [MemberData(nameof(DataGenerator.YieldReturnProviderAndZoom), MemberType = typeof(DataGenerator))]
    public async Task DownloadAllMapsImagesArchiveAsync_ShouldReturnZipArchive(int providerId, int zoomLevel)
    {
        // Arrange
        var query = new GetProvidedMapsByProviderIdQuery(providerId);
        var maps = await _mediator.Send(query).ConfigureAwait(false);

        var expectedArchiveName = $"{maps.First().MapProvider.Name}-{zoomLevel}.zip";

        // Act
        var (archiveData, archiveName) = await _downloadArchiveService.DownloadAllMapsImagesArchiveAsync(providerId, zoomLevel);

        // Assert
        Assert.Equal(expectedArchiveName, archiveName);

        using var stream = new MemoryStream(archiveData);
        using var archive = new ZipArchive(stream);
        Assert.Equal(maps.Count(), archive.Entries.Count);

        foreach (var map in maps)
        {
            var entryName = $"{map.Map.Name}.jpg";
            var entry = archive.GetEntry(entryName);
            Assert.NotNull(entry);

            await using var entryStream = entry.Open();
            using var memoryStream = new MemoryStream();
            await entryStream.CopyToAsync(memoryStream);
            var imageData = memoryStream.ToArray();
            Assert.NotEmpty(imageData);
        }
    }
}