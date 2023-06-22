using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Tests.xUnit.Domain.Entities;

public class MapProviderTests
{
    [Fact]
    public void NewMapProvider_InitializedWithDefaultValues()
    {
        // Arrange
        var mapProvider = new MapProvider();

        // Assert
        Assert.Equal(0, mapProvider.Id);
        Assert.Equal(string.Empty, mapProvider.Name);
        Assert.Equal(string.Empty, mapProvider.UrlQueryTemplate);
        Assert.Null(mapProvider.Link);
    }

    [Fact]
    public void SetProperties_ValidValues_PropertiesAreSet()
    {
        // Arrange
        var mapProvider = new MapProvider
        {
            // Act
            Id = 1,
            Name = "Provider Name",
            UrlQueryTemplate = "http://example.com/query",
            Link = "http://example.com/provider"
        };

        // Assert
        Assert.Equal(1, mapProvider.Id);
        Assert.Equal("Provider Name", mapProvider.Name);
        Assert.Equal("http://example.com/query", mapProvider.UrlQueryTemplate);
        Assert.Equal("http://example.com/provider", mapProvider.Link);
    }
}