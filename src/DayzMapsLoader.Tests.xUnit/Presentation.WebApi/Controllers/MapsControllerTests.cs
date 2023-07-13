using DayzMapsLoader.Core.Features.Maps.Queries;
using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.WebApi.Controllers.Maps;
using DayzMapsLoader.Presentation.WebApi.Controllers.ProvidedMaps;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Tests.xUnit.Presentation.WebApi.Controllers;

public class MapsControllerTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetAllMaps_ReturnsOkResultWithMaps()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var expectedMaps = new List<Map>
        {
            new() { Id = 1, Name = "Map 1" },
            new() { Id = 2, Name = "Map 2" }
        };
        mockMediator.Setup(m => m.Send(It.IsAny<GetMapsQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedMaps);

        var controller = new MapsController(mockMediator.Object);

        // Act
        var result = await controller.GetAllMaps();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualMaps = Assert.IsAssignableFrom<IEnumerable<Map>>(okResult.Value);
        Assert.Equal(expectedMaps, actualMaps);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetMapsByProviderId_ReturnsOkResultWithMaps()
    {
        // Arrange
        const int providerId = 1;
        var mockMediator = new Mock<IMediator>();
        var expectedMaps = new List<Map>
        {
            new() { Id = 1, Name = "Map 1" },
            new() { Id = 2, Name = "Map 2" }
        };
        mockMediator.Setup(m => m.Send(It.IsAny<GetMapsByProviderIdQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedMaps);

        var controller = new MapsController(mockMediator.Object);

        // Act
        var result = await controller.GetMapsByProviderId(providerId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualMaps = Assert.IsAssignableFrom<IEnumerable<Map>>(okResult.Value);
        Assert.Equal(expectedMaps, actualMaps);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetProvidedMapsByProviderId_ReturnsOkResultWithProvidedMaps()
    {
        // Arrange
        const int providerId = 1;
        var mockMediator = new Mock<IMediator>();
        var expectedProvidedMaps = new List<ProvidedMap>
        {
            new() { Id = 1, NameForProvider = "Map 1" },
            new() { Id = 2, NameForProvider = "Map 2" }
        };
        mockMediator.Setup(m => m.Send(It.IsAny<GetProvidedMapsByProviderIdQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedProvidedMaps);

        var controller = new ProvidedMapsController(mockMediator.Object);

        // Act
        var result = await controller.GetProvidedMapsByProviderId(providerId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualProvidedMaps = Assert.IsAssignableFrom<IEnumerable<ProvidedMap>>(okResult.Value);
        
        Assert.Equal(expectedProvidedMaps, actualProvidedMaps);
    }
}