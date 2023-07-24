using DayzMapsLoader.Domain.Contracts.Services;
using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Tests.xUnit.Core.TestData.MapDownload;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using System.Runtime.Versioning;

namespace DayzMapsLoader.Tests.xUnit.Core.ServicesTests;

[SupportedOSPlatform("windows")]
public class MapDownloadImageServiceTests
{
	private readonly IMapDownloadImageService _downloadImageService;
	private readonly IMediator _mediator;

	public MapDownloadImageServiceTests()
	{
		var serviceProvider = new ServiceCollection().BuildCollection();

		_downloadImageService = serviceProvider.GetRequiredService<IMapDownloadImageService>();
		_mediator = serviceProvider.GetRequiredService<IMediator>();
	}

	[Theory]
	[Trait("Category", "Integration")]
	[MemberData(nameof(DataGenerator.YieldReturnAllStages), MemberType = typeof(DataGenerator))]
	public async Task DownloadMapImageAsync_ShouldReturnExpectedMetaData(int providerId, int mapID, int typeId,
		int zoomLevel)
	{
		// Arrange
		var expectedMetaData = new MapImageMetaData(6400, 6400, 96, 96);

		// Act
		var imageData = await _downloadImageService.DownloadMapImageAsync(providerId, mapID, typeId, zoomLevel);

		// Assert
		Assert.NotNull(imageData);
		Assert.NotEmpty(imageData);

		using var ms = new MemoryStream(imageData);

		var bitmap = new Bitmap(Image.FromStream(ms));

		var metaData = new MapImageMetaData(bitmap);

		Assert.Equal(expectedMetaData, metaData);
	}

	[Theory]
	[Trait("Category", "Integration")]
	[MemberData(nameof(DataGenerator.YieldReturnProviderAndZoom), MemberType = typeof(DataGenerator))]
	public async Task DownloadAllMapImagesAsync_ShouldReturnExpectedCount(int providerId, int zoomLevel)
	{
		//Arrange
		var query = new GetProvidedMapsByProviderIdQuery(providerId);
		var expectedCount = await _mediator.Send(query).ConfigureAwait(false);

		//Act
		var images = await _downloadImageService.DownloadAllMapImagesAsync(providerId, zoomLevel);

		//Assert
		Assert.Equal(expectedCount.Count(), images.Count());
	}

	private class MapImageMetaData
	{
		public int Width { get; }

		public int Height { get; }

		public float HorizontalResolution { get; }

		public float VerticalResolution { get; }

		public MapImageMetaData(Image image)
		{
			Width = image.Width;
			Height = image.Height;
			HorizontalResolution = image.HorizontalResolution;
			VerticalResolution = image.VerticalResolution;
		}

		public MapImageMetaData(int width, int height, float horizontalResolution, float verticalResolution)
		{
			Width = width;
			Height = height;
			HorizontalResolution = horizontalResolution;
			VerticalResolution = verticalResolution;
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (MapImageMetaData)obj;

			return Width == other.Width &&
			       Height == other.Height &&
			       Math.Abs(HorizontalResolution - other.HorizontalResolution) < 0.00001 &&
			       Math.Abs(VerticalResolution - other.VerticalResolution) < 0.00001;
		}

		public override int GetHashCode()
			=> HashCode.Combine(Width, Height, HorizontalResolution, VerticalResolution);

		public override string ToString() =>
			$"Width: {Width}, Height: {Height}, HorizontalResolution: {HorizontalResolution}, VerticalResolution: {VerticalResolution}";
	}
}