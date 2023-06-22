using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Tests.xUnit.Infrastructure.RepositoriesTests;

public class MapsRepositoryTests
{
    private readonly DayzMapLoaderContext _dbContext;

    public MapsRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<DayzMapLoaderContext>()
            .UseInMemoryDatabase("MapsRepositoryTests")
            .Options;
    
        _dbContext = new DayzMapLoaderContext(dbContextOptions);
    }
    
    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAllMapsAsync_ReturnsAllMaps()
    {
        // Arrange
        var expectedMaps = new List<Map>
        {
            new() { Name =  "Test map name 1" },
            new() { Name =  "Test map name 2" },
            new() { Name =  "Test map name 3" },
        };
        
        _dbContext.Maps.AddRange(expectedMaps);
        await _dbContext.SaveChangesAsync();
        
        var repository = new MapsRepository(_dbContext);

        // Act
        var result = await repository.GetAllMapsAsync();

        // Assert
        Assert.Equal(expectedMaps.Count, result.Count());
        
        //CleanUp
        _dbContext.Maps.RemoveRange(expectedMaps);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetMapByIdAsync_ReturnsCorrectMap()
    {
        // Arrange
        var expectedMap = new Map
        {
            Name = "Test Map Name"
        };
        
        _dbContext.Maps.Add(expectedMap);
        await _dbContext.SaveChangesAsync();

        var mapId = expectedMap.Id;

        var repository = new MapsRepository(_dbContext);

        // Act
        var result = await repository.GetMapByIdAsync(mapId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMap.Id, result.Id);
        
        //CleanUp
        _dbContext.Maps.Remove(expectedMap);
        await _dbContext.SaveChangesAsync();
    }
}