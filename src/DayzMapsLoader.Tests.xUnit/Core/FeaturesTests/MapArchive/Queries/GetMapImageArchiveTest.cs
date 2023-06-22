using DayzMapsLoader.Core.Features.MapArchive.Queries;
using DayzMapsLoader.Domain.Contracts.Services;

namespace DayzMapsLoader.Tests.xUnit.Core.FeaturesTests.MapArchive.Queries;

public class GetMapImageArchiveTest
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ValidRequest_ReturnsImageDataAndName()
    {
        // Arrange
        const string expectedImageName = "map_image_archive.zip";
        var expectedImageData = Array.Empty<byte>();

        var serviceMock = new Mock<IMapDownloadArchiveService>();
        serviceMock.Setup(s => s.DownloadMapImageArchiveAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync((expectedImageData, expectedImageName));

        var handler = new GetMapImageArchiveHandler(serviceMock.Object);
        var query = new GetMapImageArchiveQuery(ProviderId: 1, MapId: 2, TypeId: 3, Zoom: 4);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(expectedImageData, result.data);
        Assert.Equal(expectedImageName, result.name);
    }
}