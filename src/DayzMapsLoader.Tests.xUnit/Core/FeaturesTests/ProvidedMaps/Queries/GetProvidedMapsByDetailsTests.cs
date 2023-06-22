using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Core.FeaturesTests.ProvidedMaps.Queries;

public class GetProvidedMapsByDetailsTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ValidRequest_ReturnsProvidedMap()
    {
        // Arrange
        const int providerId = 1;
        const int mapId = 2;
        const int typeId = 3;

        var expectedMap = new ProvidedMap
        {
            Id = 1,
            NameForProvider = "Map 1",
        };

        var repositoryMock = new Mock<IProvidedMapsRepository>();
        repositoryMock.Setup(r => r.GetProvidedMapAsync(providerId, mapId, typeId)).ReturnsAsync(expectedMap);

        var handler = new GetProvidedMapsByDetailsHandler(repositoryMock.Object);
        var query = new GetProvidedMapsQueryByDetailsQuery(providerId, mapId, typeId);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMap.Id, result.Id);
        Assert.Equal(expectedMap.NameForProvider, result.NameForProvider);
    }
}