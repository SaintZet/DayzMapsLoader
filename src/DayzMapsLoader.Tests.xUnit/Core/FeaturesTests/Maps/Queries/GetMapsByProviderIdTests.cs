using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Features.Maps.Queries;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Core.FeaturesTests.Maps.Queries;

public class GetMapsByProviderIdTests
{

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ValidRequest_ReturnMap()
    {
        // Arrange
        var expectedMap = new List<Map>
        {
            new() { Id = 1, Name = "Map 1" }
        };

        var repositoryMock = new Mock<IMapsRepository>();
        repositoryMock.Setup(r => r.GetAllMapsAsync()).ReturnsAsync(expectedMap);

        var handler = new GetMapsHandler(repositoryMock.Object);
        var query = new GetMapsQuery();
        var cancellationToken = CancellationToken.None;
    
        // Act
        var result = await handler.Handle(query, cancellationToken);
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMap.Count, result.Count());
        Assert.Equal(expectedMap.Select(m => m.Id), result.Select(m => m.Id));
    }
}