using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Core.FeaturesTests.ProvidedMaps.Queries;

public class GetProvidedMapsByMapIdTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ValidRequest_ReturnsListOfProvidedMaps()
    {
        // Arrange
        var mapId = 1;

        var expectedMaps = new List<ProvidedMap>
            {
                new ProvidedMap { Id = 1, NameForProvider = "Map 1" },
                new ProvidedMap { Id = 2, NameForProvider = "Map 2" },
            };

        var repositoryMock = new Mock<IProvidedMapsRepository>();
        repositoryMock.Setup(r => r.GetAllProvidedMapsByMapIdAsync(mapId)).ReturnsAsync(expectedMaps);

        var handler = new GetProvidedMapsByMapIdHandler(repositoryMock.Object);
        var query = new GetProvidedMapsByMapIdQuery(mapId);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMaps.Count, result.Count());
        Assert.Equal(expectedMaps.Select(m => m.Id), result.Select(m => m.Id));
    }
}