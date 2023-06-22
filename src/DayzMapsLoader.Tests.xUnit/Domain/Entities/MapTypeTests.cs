using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Domain.Entities;

public class MapTypeTests
{
    [Fact]
    public void NewMapType_InitializedWithDefaultValues()
    {
        // Arrange
        var mapType = new MapType();

        // Assert
        Assert.Equal(0, mapType.Id);
        Assert.Equal(string.Empty, mapType.Name);
    }

    [Fact]
    public void SetProperties_ValidValues_PropertiesAreSet()
    {
        // Arrange
        var mapType = new MapType
        {
            // Act
            Id = 1,
            Name = "Map Type Name"
        };

        // Assert
        Assert.Equal(1, mapType.Id);
        Assert.Equal("Map Type Name", mapType.Name);
    }
}