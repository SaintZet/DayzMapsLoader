using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Tests.xUnit.Infrastructure.RepositoriesTests;

public class MapProvidersRepositoryTests
{
    private readonly DayzMapLoaderContext _dbContext;

    public MapProvidersRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<DayzMapLoaderContext>()
            .UseInMemoryDatabase("MapProvidersRepositoryTests")
            .Options;
    
        _dbContext = new DayzMapLoaderContext(dbContextOptions);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllMapProvidersAsync_ReturnsAllMapProviders()
    {
        // Arrange
        const int expectedCount = 2; // Assuming there are 2 MapProvider entities in the database
        var mapProviders = new List<MapProvider>
        {
            new() { Name = "Map Provider 1"},
            new() { Name = "Map Provider 2"},
        };
        
        _dbContext.MapProviders.AddRange(mapProviders);
        await _dbContext.SaveChangesAsync();

        var repository = new MapProvidersRepository(_dbContext);

        // Act
        var result = await repository.GetAllMapProvidersAsync();

        // Assert
        Assert.Equal(expectedCount, result.Count());
        
        //CleanUp
        _dbContext.RemoveRange(mapProviders);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetProviderByIdAsync_ReturnsCorrectMapProvider()
    {
        // Arrange
        var expectedProvider = new MapProvider { Name = "Test Provider Name" };

        _dbContext.MapProviders.Add(expectedProvider);
        await _dbContext.SaveChangesAsync();

        var providerId = expectedProvider.Id;

        var repository = new MapProvidersRepository(_dbContext);

        // Act
        var result = await repository.GetProviderByIdAsync(providerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedProvider.Name, result.Name);
        
        //CleanUp
        _dbContext.Remove(expectedProvider);
        await _dbContext.SaveChangesAsync();
    }
}