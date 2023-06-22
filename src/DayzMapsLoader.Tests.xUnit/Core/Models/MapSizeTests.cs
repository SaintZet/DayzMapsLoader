using DayzMapsLoader.Core.Models;

namespace DayzMapsLoader.Tests.xUnit.Core.Models;

public class MapSizeTests
{
    [Fact]
    [Trait("Category", "Unity")]
    public void MapSize_Constructor_SetsWidthAndHeight()
    {
        // Arrange
        const int width = 800;
        const int height = 600;

        // Act
        var mapSize = new MapSize(width, height);

        // Assert
        Assert.Equal(width, mapSize.Width);
        Assert.Equal(height, mapSize.Height);
    }

    [Theory]
    [Trait("Category", "Unity")]
    [InlineData(0, 1, 1)]
    [InlineData(1, 2, 2)]
    [InlineData(2, 4, 4)]
    [InlineData(3, 8, 8)]
    public void MapSize_ConvertZoomLevelRatioSize_ReturnsCorrectMapSize(int zoomLevel, int expectedWidth, int expectedHeight)
    {
        // Act
        var mapSize = MapSize.ConvertZoomLevelRatioSize(zoomLevel);

        // Assert
        Assert.Equal(expectedWidth, mapSize.Width);
        Assert.Equal(expectedHeight, mapSize.Height);
    }
}