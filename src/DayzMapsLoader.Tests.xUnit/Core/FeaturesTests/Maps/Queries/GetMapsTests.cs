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
            new() { Id = 1, Name = "Map 1" },
            new() { Id = 2, Name = "Map 2" },
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
    
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ReturnsAllMaps()
    {
        // Arrange
        var maps = new List<Map>
        {
            new() { Id = 1, Name = "Map 1" },
            new() { Id = 2, Name = "Map 2" },
            new() { Id = 3, Name = "Map 3" }
        };

        var mapsRepositoryMock = new Mock<IMapsRepository>();
        mapsRepositoryMock.Setup(repo => repo.GetAllMapsAsync()).ReturnsAsync(maps);

        var handler = new GetMapsHandler(mapsRepositoryMock.Object);

        var query = new GetMapsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(3, result.Count());
        Assert.Contains(result, map => map is { Id: 1, Name: "Map 1" });
        Assert.Contains(result, map => map is { Id: 2, Name: "Map 2" });
        Assert.Contains(result, map => map is { Id: 3, Name: "Map 3" });
    }
}