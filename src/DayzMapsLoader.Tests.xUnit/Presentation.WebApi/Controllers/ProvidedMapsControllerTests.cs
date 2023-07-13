using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.WebApi.Controllers.ProvidedMaps;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Tests.xUnit.Presentation.WebApi.Controllers;

public class ProvidedMapsControllerTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetAllProvidedMaps_ReturnsOkResultWithProvidedMaps()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var expectedProvidedMaps = new List<ProvidedMap>
        {
            new() { Id = 1, NameForProvider = "Map 1" },
            new() { Id = 2, NameForProvider = "Map 2" }
        };
        mockMediator.Setup(m => m.Send(It.IsAny<GetProvidedMapsQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedProvidedMaps);

        var controller = new ProvidedMapsController(mockMediator.Object);

        // Act
        var result = await controller.GetAllProvidedMaps();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualProvidedMaps = Assert.IsAssignableFrom<IEnumerable<ProvidedMap>>(okResult.Value);
        Assert.Equal(expectedProvidedMaps, actualProvidedMaps);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetProvidedMapsByMapId_ReturnsOkResultWithProvidedMaps()
    {
        // Arrange
        const int mapId = 1;
        var mockMediator = new Mock<IMediator>();
        var expectedProvidedMaps = new List<ProvidedMap>
        {
            new() { Id = 1, NameForProvider = "Map 1" },
            new() { Id = 2, NameForProvider = "Map 2" }
        };
        mockMediator.Setup(m => m.Send(It.IsAny<GetProvidedMapsByMapIdQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedProvidedMaps);

        var controller = new ProvidedMapsController(mockMediator.Object);

        // Act
        var result = await controller.GetProvidedMapsByMapId(mapId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualProvidedMaps = Assert.IsAssignableFrom<IEnumerable<ProvidedMap>>(okResult.Value);
        Assert.Equal(expectedProvidedMaps, actualProvidedMaps);
    }
}