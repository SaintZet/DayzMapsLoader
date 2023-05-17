using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Features.Maps.Queries;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Core.FeaturesTests.Maps.Queries;

public class GetMapsTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ValidRequest_ReturnsListOfMaps()
    {
        // Arrange
        var expectedMaps = new List<Map>
            {
                new Map { Id = 1, Name = "Map 1" },
                new Map { Id = 2, Name = "Map 2" },
            };

        var repositoryMock = new Mock<IMapsRepository>();
        repositoryMock.Setup(r => r.GetAllMapsAsync()).ReturnsAsync(expectedMaps);

        var handler = new GetMapsHandler(repositoryMock.Object);
        var query = new GetMapsQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMaps.Count, result.Count());
        Assert.Equal(expectedMaps.Select(m => m.Id), result.Select(m => m.Id));
    }
}