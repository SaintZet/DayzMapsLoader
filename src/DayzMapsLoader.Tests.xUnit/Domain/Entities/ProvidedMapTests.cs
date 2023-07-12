using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Domain.Entities;

public class ProvidedMapTests
{
    [Fact]
    public void NewProvidedMap_InitializedWithDefaultValues()
    {
        // Arrange
        var providedMap = new ProvidedMap();

        // Assert
        Assert.Equal(0, providedMap.Id);
        Assert.Equal(string.Empty, providedMap.NameForProvider);
        Assert.Equal(default(MapProvider), providedMap.MapProvider);
        Assert.Equal(default(Map), providedMap.MapData);
        Assert.Equal(default(MapType), providedMap.MapType);
        Assert.Equal(string.Empty, providedMap.MapTypeForProvider);
        Assert.Equal(0, providedMap.MaxMapLevel);
        Assert.False(providedMap.IsFirstQuadrant);
        Assert.Equal(string.Empty, providedMap.Version);
        Assert.Equal(string.Empty, providedMap.ImageExtension);
    }

    [Fact]
    public void SetProperties_ValidValues_PropertiesAreSet()
    {
        // Arrange
        var providedMap = new ProvidedMap();
        var mapProvider = new MapProvider();
        var map = new Map();
        var mapType = new MapType();

        // Act
        providedMap.Id = 1;
        providedMap.NameForProvider = "Map Name";
        providedMap.MapProvider = mapProvider;
        providedMap.MapData = map;
        providedMap.MapType = mapType;
        providedMap.MapTypeForProvider = "Map Type";
        providedMap.MaxMapLevel = 10;
        providedMap.IsFirstQuadrant = true;
        providedMap.Version = "1.0";
        providedMap.ImageExtension = ".png";

        // Assert
        Assert.Equal(1, providedMap.Id);
        Assert.Equal("Map Name", providedMap.NameForProvider);
        Assert.Equal(mapProvider, providedMap.MapProvider);
        Assert.Equal(map, providedMap.MapData);
        Assert.Equal(mapType, providedMap.MapType);
        Assert.Equal("Map Type", providedMap.MapTypeForProvider);
        Assert.Equal(10, providedMap.MaxMapLevel);
        Assert.True(providedMap.IsFirstQuadrant);
        Assert.Equal("1.0", providedMap.Version);
        Assert.Equal(".png", providedMap.ImageExtension);
    }
}