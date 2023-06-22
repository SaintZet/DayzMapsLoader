using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Tests.xUnit.Infrastructure.RepositoriesTests;

public class ProvidedMapsRepositoryTests
{

    private readonly DayzMapLoaderContext _dbContext;

    public ProvidedMapsRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<DayzMapLoaderContext>()
            .UseInMemoryDatabase("ProvidedMapsRepositoryTests")
            .Options;
        
        _dbContext = new DayzMapLoaderContext(dbContextOptions);
    }

    
    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllProvidedMapsAsync_ReturnsAllProvidedMaps()
    {
        // Arrange
        var expectedProvidedMaps = new List<ProvidedMap>
        {
            new()
            {
                MapProvider = new MapProvider { Name = "Test provider name 1" },
                Map = new Map { Name = "Test map name 1" },
                MapType = new MapType { Name = "Test map type 1" },
            },
            new()
            {
                MapProvider = new MapProvider { Name = "Test provider name 2" },
                Map = new Map { Name = "Test map name 2" },
                MapType = new MapType { Name = "Test map type 2" },
            },
        };
        
        _dbContext.ProvidedMaps.AddRange(expectedProvidedMaps);
        await _dbContext.SaveChangesAsync();
        
        var repository = new ProvidedMapsRepository(_dbContext);

        // Act
        var result = await repository.GetAllProvidedMapsAsync();

        // Assert
        Assert.Equal(expectedProvidedMaps.Count, result.Count());
        
        //CleanUp
        _dbContext.ProvidedMaps.RemoveRange(expectedProvidedMaps);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllProvidedMapsByMapIdAsync_ReturnsProvidedMapsByMapId()
    {
        // Arrange
        var map = new Map { Name = "Test map name 1" };
        
        var expectedProvidedMaps = new List<ProvidedMap>
        {
            new()
            {
                MapProvider = new MapProvider { Name = "Test provider name 1" },
                Map = map,
                MapType = new MapType { Name = "Test map type 1" },
            },
            new()
            {
                MapProvider = new MapProvider { Name = "Test provider name 2" },
                Map = map,
                MapType = new MapType { Name = "Test map type 2" },
            },
        };
        
        _dbContext.ProvidedMaps.AddRange(expectedProvidedMaps);
        await _dbContext.SaveChangesAsync();
        
        var repository = new ProvidedMapsRepository(_dbContext);
        
        var mapId = expectedProvidedMaps.First(x => x.Map.Name == "Test map name 1").Map.Id;
        // Act
        var result = await repository.GetAllProvidedMapsByMapIdAsync(mapId);

        // Assert
        Assert.Equal(expectedProvidedMaps.Count, result.Count());
        
        //CleanUp
        _dbContext.ProvidedMaps.RemoveRange(expectedProvidedMaps);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllProvidedMapsByProviderIdAsync_ReturnsCorrectProvidedMaps()
    {
        // Arrange
        var provider = new MapProvider { Name = "Test provider name " };
        var expectedProvidedMaps = new List<ProvidedMap>
        {
            new()
            {
                MapProvider = provider,
                Map = new Map { Name = "Test map name 1" },
                MapType = new MapType { Name = "Test map type 1" },
            },
            new()
            {
                MapProvider = provider,
                Map = new Map { Name = "Test map name 2" },
                MapType = new MapType { Name = "Test map type 2" },
            },
        };
        
        _dbContext.ProvidedMaps.AddRange(expectedProvidedMaps);
        await _dbContext.SaveChangesAsync();

        var providerId = expectedProvidedMaps.First().MapProvider.Id;

        IProvidedMapsRepository repository = new ProvidedMapsRepository(_dbContext);

        // Act
        var result = await repository.GetAllProvidedMapsByProviderIdAsync(providerId);

        // Assert
        Assert.Equal(expectedProvidedMaps.Count, result.Count());
        Assert.Equal(expectedProvidedMaps.Select(x => x.Id), result.Select(x => x.Id));
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetProvidedMapAsync_ReturnsCorrectProvidedMap()
    {
        // Arrange
        var expectedProvidedMap = new ProvidedMap
        {
            MapProvider = new MapProvider(),
            Map = new Map(),
            MapType = new MapType(),
        };
        
        _dbContext.ProvidedMaps.Add(expectedProvidedMap);
        await _dbContext.SaveChangesAsync();

        var providerId = expectedProvidedMap.MapProvider.Id;
        var mapId = expectedProvidedMap.Map.Id;
        var typeId = expectedProvidedMap.MapType.Id;

        IProvidedMapsRepository repository = new ProvidedMapsRepository(_dbContext);

        // Act
        var result = await repository.GetProvidedMapAsync(providerId, mapId, typeId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProvidedMap.Id, result.Id);
        Assert.Equal(expectedProvidedMap.MapProvider, result.MapProvider);
        Assert.Equal(expectedProvidedMap.Map, result.Map);
        Assert.Equal(expectedProvidedMap.MapType, result.MapType);
    }
    
    [Fact]
    public async Task GetProvidedMapsByProviderId_ReturnsCorrectMaps()
    {
        // Arrange
        var mapProvider1 =  new MapProvider { Name = "Test map provider 1" };
        var mapProvider2 = new MapProvider { Name = "Test map provider 2" };
        
        var expectedMaps = new List<ProvidedMap>
        {
            new()
            {
                MapProvider = mapProvider1,
                Map = new Map { Name = "Test map name 1" },
                MapType = new MapType { Name = "Test map type 1" },
            },
            new()
            {
                MapProvider = mapProvider1,
                Map = new Map { Name = "Test map name 2" },
                MapType = new MapType { Name = "Test map type 2" },
            },
        };

        var anotherMap = new ProvidedMap 
        {
            MapProvider = mapProvider2,
            Map = new Map { Name = "Test map name 3" },
            MapType = new MapType { Name = "Test map type 3" },
        };

        var mapPull = expectedMaps.Concat(new List<ProvidedMap> { anotherMap } ).ToList();
        
        _dbContext.ProvidedMaps.AddRange(mapPull);
        await _dbContext.SaveChangesAsync();
        
        var mapProviderId = expectedMaps.First(x => x.MapProvider.Name == "Test map provider 1").MapProvider.Id;

        IProvidedMapsRepository repository = new ProvidedMapsRepository(_dbContext);

        // Act
        var result = await repository.GetMapsByProviderId(mapProviderId);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(expectedMaps.Select(x => x.Map.Name), result.Select(x => x.Name));
    }
}