using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Tests.xUnit.Infrastructure.RepositoriesTests;

public class BaseRepositoryTests
{
    private readonly DayzMapLoaderContext _dbContext;

    public BaseRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<DayzMapLoaderContext>()
            .UseInMemoryDatabase("BaseRepositoryTests")
            .Options;

        _dbContext = new DayzMapLoaderContext(dbContextOptions);
    }
    
    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAll_ReturnsAllEntities_WhenDatabaseIsNotEmpty()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);

        var entity1 = new Map();
        var entity2 = new Map();
        
        _dbContext.AddRange(entity1, entity2);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAll().ToListAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(entity1, result);
        Assert.Contains(entity2, result);
        
        //CleanUp
        _dbContext.RemoveRange(entity1, entity2);
        await _dbContext.SaveChangesAsync();
    }
    
    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAll_ReturnsEmptyList_WhenDatabaseIsEmpty()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);

        // Act
        var result = await repository.GetAll().ToListAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAll_ReturnsExpectedEntities()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);

        var entity1 = new Map { Name = "Map 1" };
        var entity2 = new Map { Name = "Map 2" };
        
        _dbContext.AddRange(entity1, entity2);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAll().ToListAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, x => x.Name == "Map 1");
        Assert.Contains(result, x => x.Name == "Map 2");
        
        //CleanUp
        _dbContext.RemoveRange(entity1, entity2);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task AddAsync_AddsEntityToDatabase()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);
        var entity = new Map { Name = "Test map name"};

        // Act
        var result = await repository.AddAsync(entity);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);
        
        //CleanUp
        await repository.DeleteAsync(entity);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task AddAsync_ThrowsException_WhenEntityIsNull()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.AddAsync(null));
    }
    
    [Fact]
    [Trait("Category", "Integration")]
    public async Task AddAsync_ThrowsException_WhenEntityWithSameIdAlreadyExists()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);
        var entity = new Map { Id = 1, Name = "Existing map" };
        await repository.AddAsync(entity); // Add entity with same ID

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.AddAsync(entity));

        // CleanUp
        await repository.DeleteAsync(entity);
    }
    
    [Fact]
    [Trait("Category", "Integration")]
    public async Task AddAsync_AddsEntityToEmptyDatabase()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);
        var entity = new Map();

        // Act
        var result = await repository.AddAsync(entity);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);

        // Check if the entity is added to the database
        var addedEntity = await _dbContext.Set<Map>().FindAsync(result.Id);
        Assert.NotNull(addedEntity);
        Assert.Equal(result.Id, addedEntity.Id);
        
        //CleanUp
        await repository.DeleteAsync(entity);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task AddAsync_UpdatesExistingEntity()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);
        var existingEntity = new Map();
        
        await repository.AddAsync(existingEntity);

        // Update the existing entity
        existingEntity.Name = "Updated Name";

        // Act
        var result = await repository.UpdateAsync(existingEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingEntity.Id, result.Id);

        // Check if the entity is updated in the database
        var updatedEntity = await _dbContext.Set<Map>().FindAsync(existingEntity.Id);
        Assert.NotNull(updatedEntity);
        Assert.Equal(existingEntity.Name, updatedEntity.Name);
        
        _dbContext.Remove(existingEntity);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task UpdateAsync_UpdatesEntityInDatabase()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);
        var entity = new Map();
        
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.UpdateAsync(entity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        
        //CleanUp
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task UpdateAsync_ThrowsException_WhenEntityIsNull()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.UpdateAsync(null));
    }
    
    [Fact]
    [Trait("Category", "Integration")]
    public async Task DeleteAsync_DeletesExistingEntity()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);
        var entity = new Map { Name = "Test map name" };
        await repository.AddAsync(entity);

        // Act
        await repository.DeleteAsync(entity);

        // Assert
        var deletedEntity = await _dbContext.Set<Map>().FindAsync(entity.Id);
        Assert.Null(deletedEntity);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task DeleteAsync_ThrowsException_WhenEntityIsNull()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.DeleteAsync(null));
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task DeleteAsync_ThrowsException_WhenEntityNotFound()
    {
        // Arrange
        var repository = new BaseRepository<Map>(_dbContext);
        var entity = new Map { Id = 1, Name = "Non-existing map" };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () => await repository.DeleteAsync(entity));
        Assert.Equal($"Entity could not be deleted: Entity with ID {entity.Id} does not exist in the database.", exception.Message);
    }
}