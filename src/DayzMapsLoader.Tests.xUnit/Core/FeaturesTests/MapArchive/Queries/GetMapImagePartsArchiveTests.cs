using DayzMapsLoader.Core.Contracts.Services;
using DayzMapsLoader.Core.Features.MapArchive.Queries;

namespace DayzMapsLoader.Tests.xUnit.Core.FeaturesTests.MapArchive.Queries;

public class GetMapImagePartsArchiveTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ValidRequest_ReturnsImageDataAndName()
    {
        // Arrange
        const string expectedImageName = "map_image_parts_archive.zip";
        var expectedImageData = Array.Empty<byte>();

        var mapDownloaderMock = new Mock<IMapDownloadArchiveService>();
        mapDownloaderMock
            .Setup(x => x.DownloadMapImagePartsArchiveAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((expectedImageData, expectedImageName));

        var handler = new GetMapImagePartsArchiveHandler(mapDownloaderMock.Object);
        var query = new GetMapImagePartsArchiveQuery(ProviderId: 1, MapId: 2, TypeId: 3, Zoom: 4);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(expectedImageData, result.data);
        Assert.Equal(expectedImageName, result.name);
    }
}