using DayzMapsLoader.Core.Features.MapArchive.Queries;
using DayzMapsLoader.Domain.Contracts.Services;

namespace DayzMapsLoader.Tests.xUnit.Core.FeaturesTests.MapArchive.Queries;

public class GetAllMapsImagesArchiveTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ValidRequest_ReturnsImageDataAndName()
    {
        // Arrange
        var expectedImageData = new byte[] { /* Image data bytes */ };
        var expectedImageName = "map_archive.zip";

        var serviceMock = new Mock<IMapDownloadArchiveService>();
        serviceMock.Setup(s => s.DownloadAllMapsImagesArchiveAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((expectedImageData, expectedImageName));

        var handler = new GetAllMapsImagesArchiveHandler(serviceMock.Object);
        var query = new GetAllMapsImagesArchiveQuery(ProviderId: 1, Zoom: 2);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(expectedImageData, result.data);
        Assert.Equal(expectedImageName, result.name);
    }
}