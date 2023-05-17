using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Features.MapProviders.Queries;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Core.FeaturesTests.MapProviders.Queries;

public class GetMapProvidersTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ValidRequest_ReturnsListOfMapProviders()
    {
        // Arrange
        var expectedMapProviders = new List<MapProvider>
            {
                new MapProvider { Id = 1, Name = "Provider 1" },
                new MapProvider { Id = 2, Name = "Provider 2" },
            };

        var repositoryMock = new Mock<IMapProvidersRepository>();
        repositoryMock.Setup(r => r.GetAllMapProvidersAsync()).ReturnsAsync(expectedMapProviders);

        var handler = new GetMapProvidersHandler(repositoryMock.Object);
        var query = new GetMapProvidersQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMapProviders.Count, result.Count());
        Assert.Equal(expectedMapProviders.Select(mp => mp.Id), result.Select(mp => mp.Id));
    }
}