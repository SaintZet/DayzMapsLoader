using DayzMapsLoader.Core.Features.MapProviders.Queries;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Presentation.WebApi.Controllers.MapProviders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Tests.xUnit.Presentation.WebApi.Controllers;

public class MapProvidersControllerTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetAllMapProviders_ReturnsOkResultWithMapProviders()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var expectedMapProviders = new List<MapProvider>
        {
            new() { Id = 1, Name = "Provider 1" },
            new() { Id = 2, Name = "Provider 2" }
        };
        mockMediator.Setup(m => m.Send(It.IsAny<GetMapProvidersQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedMapProviders);

        var controller = new MapProvidersController(mockMediator.Object);

        // Act
        var result = await controller.GetAllMapProviders();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualMapProviders = Assert.IsAssignableFrom<IEnumerable<MapProvider>>(okResult.Value);
        Assert.Equal(expectedMapProviders, actualMapProviders);
    }
}