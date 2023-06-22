using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Domain.Entities;

public class MapTests
{
    [Fact]
    public void NewMap_InitializedWithDefaultValues()
    {
        // Arrange
        var map = new Map();

        // Assert
        Assert.Equal(0, map.Id);
        Assert.Equal(string.Empty, map.Name);
        Assert.Null(map.LastUpdate);
        Assert.Null(map.Author);
        Assert.Null(map.Link);
    }

    [Fact]
    public void SetProperties_ValidValues_PropertiesAreSet()
    {
        // Arrange
        var map = new Map();
        var lastUpdate = DateTime.Now;

        // Act
        map.Id = 1;
        map.Name = "Map Name";
        map.LastUpdate = lastUpdate;
        map.Author = "Author";
        map.Link = "http://example.com";

        // Assert
        Assert.Equal(1, map.Id);
        Assert.Equal("Map Name", map.Name);
        Assert.Equal(lastUpdate, map.LastUpdate);
        Assert.Equal("Author", map.Author);
        Assert.Equal("http://example.com", map.Link);
    }
}