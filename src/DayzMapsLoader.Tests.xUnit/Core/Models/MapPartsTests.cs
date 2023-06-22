using DayzMapsLoader.Core.Models;

namespace DayzMapsLoader.Tests.xUnit.Core.Models;

public class MapPartsTests
{
    [Fact]
    [Trait("Category", "Unity")]
    public void MapParts_InitializesCorrectly()
    {
        // Arrange
        var size = new MapSize(3, 3);

        // Act
        var mapParts = new MapParts(size);

        // Assert
        Assert.Equal(size.Width, mapParts.Weight);
        Assert.Equal(size.Height, mapParts.Height);
    }

    [Fact]
    [Trait("Category", "Unity")]
    public void AddPart_AddsMapPartCorrectly()
    {
        // Arrange
        var size = new MapSize(3, 3);
        var mapParts = new MapParts(size);
        var mapPart = new MapPart(new byte[]{ 0, 1});

        // Act
        mapParts.AddPart(1, 1, mapPart);

        // Assert
        Assert.Equal(mapPart, mapParts.GetPartOfMap(1, 1));
    }
}