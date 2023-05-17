using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Core.FeaturesTests.ProvidedMaps.Queries;

public class GetProvidedMapsTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ReturnsListOfProvidedMaps()
    {
        // Arrange
        var providedMapsRepositoryMock = new Mock<IProvidedMapsRepository>();
        var expectedProvidedMaps = new List<ProvidedMap>
        {
            new ProvidedMap { Id = 1, NameForProvider = "Map 1" },
            new ProvidedMap { Id = 2, NameForProvider = "Map 2" },
        };
        providedMapsRepositoryMock.Setup(repo => repo.GetAllProvidedMapsAsync()).ReturnsAsync(expectedProvidedMaps);

        var query = new GetProvidedMapsQuery();
        var handler = new GetProvidedMapsHandler(providedMapsRepositoryMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expectedProvidedMaps, result);
    }
}