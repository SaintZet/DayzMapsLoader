using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;
using DayzMapsLoader.Tests.xUnit.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Tests.xUnit.Infrastructure.RepositoriesTests;

public class ProvidedMapsRepositoryTests
{
    private readonly DayzMapLoaderContext _dbContext;

    public ProvidedMapsRepositoryTests()
    {
        var serviceProvider = new ServiceCollection().BuildCollection();
        _dbContext = serviceProvider.GetService<DayzMapLoaderContext>()!;
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllProvidedMapsAsync_ReturnsAllProvidedMaps()
    {
        // Arrange
        const int expectedCount = 24; // Assuming there are 24 ProvidedMap entities in the database
        
        var repository = new ProvidedMapsRepository(_dbContext);

        // Act
        var result = await repository.GetAllProvidedMapsAsync();

        // Assert
        Assert.Equal(expectedCount, result.Count());
    }
    
    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllProvidedMapsByMapIdAsync_ReturnsProvidedMapsByMapId()
    {
        // Arrange
        const int mapId = 1; // Assuming there are ProvidedMap entities with MapId = 1 in the database
        const int expectedCount = 4; // Assuming there are 4 ProvidedMap entities with MapId = 1
        
        var repository = new ProvidedMapsRepository(_dbContext);

        // Act
        var result = await repository.GetAllProvidedMapsByMapIdAsync(mapId);

        // Assert
        Assert.Equal(expectedCount, result.Count());
    }
    
    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllProvidedMapsByProviderIdAsync_ReturnsCorrectProvidedMaps()
    {
        // Arrange
        const int providerId = 1;
        var expectedProvidedMaps = new List<ProvidedMap>
        {
            new ProvidedMap { Id = 1, MapProvider = new MapProvider {Id = providerId} },
            new ProvidedMap { Id = 2, MapProvider = new MapProvider {Id = providerId} },
        };

        await using (_dbContext)
        {
            _dbContext.ProvidedMaps.AddRange(expectedProvidedMaps);
            await _dbContext.SaveChangesAsync();
        }

        await using (_dbContext)
        {
            IProvidedMapsRepository repository = new ProvidedMapsRepository(_dbContext);

            // Act
            var result = await repository.GetAllProvidedMapsByProviderIdAsync(providerId);

            // Assert
            Assert.Equal(expectedProvidedMaps.Count, result.Count());
            Assert.Equal(expectedProvidedMaps.Select(x => x.Id), result.Select(x => x.Id));
        }
    }
    
    [Fact]
    
    public async Task GetProvidedMapAsync_ReturnsCorrectProvidedMap()
    {
        // Arrange
        const int providerId = 1;
        const int mapId = 2;
        const int typeId = 3;
        var expectedProvidedMap = new ProvidedMap
        {
            Id = 1,
            MapProvider = new MapProvider(),
            Map = new Map(),
            MapType = new MapType(),
        };

        await using (_dbContext)
        {
            _dbContext.ProvidedMaps.Add(expectedProvidedMap);
            await _dbContext.SaveChangesAsync();
        }

        await using (_dbContext)
        {
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
    }
}