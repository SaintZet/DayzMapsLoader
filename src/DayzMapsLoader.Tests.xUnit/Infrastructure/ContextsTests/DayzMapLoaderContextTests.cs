using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Tests.xUnit.Infrastructure.ContextsTests;

public class DayzMapLoaderContextTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public void Maps_Getter_ReturnsDbSetOfMaps()
    {
        // Arrange
        var dbContext = new DayzMapLoaderContext();
            
        // Act
        var result = dbContext.Maps;
            
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<DbSet<Map>>(result);
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void Maps_Setter_SetsDbSetOfMaps()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DayzMapLoaderContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new DayzMapLoaderContext(options);
        var mapsDbSet = dbContext.Maps;

        // Act & Assert
        Assert.NotNull(mapsDbSet);
        Assert.IsAssignableFrom<DbSet<Map>>(mapsDbSet);
    }
        
    [Fact]
    [Trait("Category", "Unit")]
    public void MapProviders_Getter_ReturnsDbSetOfMapProviders()
    {
        // Arrange
        var dbContext = new DayzMapLoaderContext();
            
        // Act
        var result = dbContext.MapProviders;
            
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<DbSet<MapProvider>>(result);
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void MapProviders_Setter_SetsDbSetOfMapProviders()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DayzMapLoaderContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new DayzMapLoaderContext(options);
        var mapProvidersDbSet = dbContext.MapProviders;

        // Act & Assert
        Assert.NotNull(mapProvidersDbSet);
        Assert.IsAssignableFrom<DbSet<MapProvider>>(mapProvidersDbSet);
    }
        
    [Fact]
    [Trait("Category", "Unit")]
    public void ProvidedMaps_Getter_ReturnsDbSetOfProvidedMaps()
    {
        // Arrange
        var dbContext = new DayzMapLoaderContext();
            
        // Act
        var result = dbContext.ProvidedMaps;
            
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<DbSet<ProvidedMap>>(result);
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void ProvidedMaps_Setter_SetsDbSetOfProvidedMaps()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DayzMapLoaderContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new DayzMapLoaderContext(options);
        var providedMapsDbSet = dbContext.ProvidedMaps;

        // Act & Assert
        Assert.NotNull(providedMapsDbSet);
        Assert.IsAssignableFrom<DbSet<ProvidedMap>>(providedMapsDbSet);
    }
        
    [Fact]
    [Trait("Category", "Unit")]
    public void MapTypes_Getter_ReturnsDbSetOfMapTypes()
    {
        // Arrange
        var dbContext = new DayzMapLoaderContext();
            
        // Act
        var result = dbContext.MapTypes;
            
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<DbSet<MapType>>(result);
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void MapTypes_Setter_SetsDbSetOfMapTypes()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DayzMapLoaderContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new DayzMapLoaderContext(options);
        var mapTypesDbSet = dbContext.MapTypes;

        // Act & Assert
        Assert.NotNull(mapTypesDbSet);
        Assert.IsAssignableFrom<DbSet<MapType>>(mapTypesDbSet);
    }
}