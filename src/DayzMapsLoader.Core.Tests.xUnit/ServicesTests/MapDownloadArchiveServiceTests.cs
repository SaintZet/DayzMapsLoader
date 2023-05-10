using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Services;
using DayzMapsLoader.Core.Extensions;
using DayzMapsLoader.Core.Tests.xUnit.TestData.MapDownload;
using DayzMapsLoader.Infrastructure.Extensions;
using DayzMapsLoader.Shared.Wrappers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.IO.Compression;

namespace DayzMapsLoader.Core.Tests.xUnit.ServicesTests;

public class MapDownloadArchiveServiceTests
{
    private readonly IMapDownloadArchiveService _downloadArchiveService;
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public MapDownloadArchiveServiceTests()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddApplicationLayer();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddInfrastractureLayer(config.GetConnectionString("DefaultConnection")!);

        var serviceProvider = services.BuildServiceProvider();

        _downloadArchiveService = serviceProvider.GetRequiredService<IMapDownloadArchiveService>();
        _providedMapsRepository = serviceProvider.GetRequiredService<IProvidedMapsRepository>();
    }

    [Theory]
    [Trait("Category", "Integration")]
    [MemberData(nameof(DataGenerator.YieldReturnAllStages), MemberType = typeof(DataGenerator))]
    public async Task DownloadMapImageArchiveAsync_ShouldReturnZipArchive(int providerId, int mapId, int typeId, int zoom)
    {
        // Arrange
        var map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapId, typeId).ConfigureAwait(false);
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

        using var entryStream = entry.Open();
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
        var map = await _providedMapsRepository.GetProvidedMapAsync(providerId, mapId, typeId).ConfigureAwait(false);
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

            using var entryStream = entry.Open();
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
        var maps = await _providedMapsRepository.GetAllProvidedMapsByProviderIdAsync(providerId).ConfigureAwait(false);
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

            using var entryStream = entry.Open();
            using var memoryStream = new MemoryStream();
            await entryStream.CopyToAsync(memoryStream);
            var imageData = memoryStream.ToArray();
            Assert.NotEmpty(imageData);
        }
    }
}