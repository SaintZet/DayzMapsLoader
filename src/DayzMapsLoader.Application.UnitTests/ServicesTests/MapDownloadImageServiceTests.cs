using DayzMapsLoader.Application.Abstractions.Infrastructure.Repositories;
using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Extensions;
using DayzMapsLoader.Application.Tests.ServicesTests;
using DayzMapsLoader.Application.Tests.TestData.MapDownload;
using DayzMapsLoader.Infrastructure.Extensions;
using DayzMapsLoader.Presentation.WebApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Application.UnitTests.Services;

[SupportedOSPlatform("windows")]
public class MapDownloadImageServiceTests
{
    private readonly IMapDownloadImageService _downloadImageService;
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public MapDownloadImageServiceTests()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        IServiceCollection services = new ServiceCollection();
        services.AddApplicationLayer();
        services.AddInfrastractureLayer();
        services.AddDatabase(config);

        var serviceProvider = services.BuildServiceProvider();

        _downloadImageService = serviceProvider.GetRequiredService<IMapDownloadImageService>();
        _providedMapsRepository = serviceProvider.GetRequiredService<IProvidedMapsRepository>();
    }

    [Theory]
    [Trait("Category", "Integration")]
    [MemberData(nameof(DataGenerator.YieldReturnAllStages), MemberType = typeof(DataGenerator))]
    public async Task DownloadMapImageAsync_ShouldReturnExpectedMetaData(int providerId, int mapID, int typeId, int zoomLevel)
    {
        // Arrange
        var expectedMetaData = new MapImageMetaData
        {
            Width = 6400,
            Height = 6400,
            HorizontalResolution = 96,
            VerticalResolution = 96,
        };

        // Act
        var imageData = await _downloadImageService.DownloadMapImageAsync(providerId, mapID, typeId, zoomLevel);

        // Assert
        Assert.NotNull(imageData);
        Assert.NotEmpty(imageData);

        using var ms = new MemoryStream(imageData);

        var bitmap = new Bitmap(Image.FromStream(ms));

        var metaData = new MapImageMetaData
        {
            Width = bitmap.Width,
            Height = bitmap.Height,
            HorizontalResolution = bitmap.HorizontalResolution,
            VerticalResolution = bitmap.VerticalResolution,
        };

        Assert.Equal(expectedMetaData, metaData);
    }

    [Theory]
    [Trait("Category", "Integration")]
    [MemberData(nameof(DataGenerator.YieldReturnAllStages), MemberType = typeof(DataGenerator))]
    public async Task DownloadMapImageInPartsAsync_ShouldReturnExpectedMetaData(int providerId, int mapID, int typeId, int zoomLevel)
    {
        // Arrange
        var expectedMetaData = new MapImageMetaData
        {
            Width = zoomLevel * 2,
            Height = zoomLevel * 2,
            HorizontalResolution = 96,
            VerticalResolution = 96,
        };

        // Act
        var mapPartsData = await _downloadImageService.DownloadMapImageInPartsAsync(providerId, mapID, typeId, zoomLevel);

        // Assert
        Assert.NotNull(mapPartsData);
        Assert.NotEmpty(mapPartsData);

        var metaData = new MapImageMetaData
        {
            Width = mapPartsData.GetLength(1),
            Height = mapPartsData.GetLength(0),
            HorizontalResolution = expectedMetaData.HorizontalResolution,
            VerticalResolution = expectedMetaData.VerticalResolution,
        };

        Assert.Equal(expectedMetaData, metaData);
    }

    [Theory]
    [Trait("Category", "Integration")]
    [MemberData(nameof(DataGenerator.YieldReturnProviderAndZoom), MemberType = typeof(DataGenerator))]
    public async Task DownloadAllMapImagesAsync_ShouldReturnExpectedCount(int providerId, int zoomLevel)
    {
        //Arrange
        var expectedCount = await _providedMapsRepository.GetAllProvidedMapsByProviderIdAsync(providerId);

        //Act
        var images = await _downloadImageService.DownloadAllMapImagesAsync(providerId, zoomLevel);

        //Assert
        Assert.Equal(expectedCount.Count(), images.Count());
    }

    private class MapImageMetaData
    {
        public int Width;
        public int Height;
        public float HorizontalResolution;
        public float VerticalResolution;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (MapImageMetaData)obj;

            return Width == other.Width &&
                   Height == other.Height &&
                   HorizontalResolution == other.HorizontalResolution &&
                   VerticalResolution == other.VerticalResolution;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height, HorizontalResolution, VerticalResolution);
        }

        public override string ToString()
        {
            return $"Width: {Width}, Height: {Height}, HorizontalResolution: {HorizontalResolution}, VerticalResolution: {VerticalResolution}";
        }
    }
}