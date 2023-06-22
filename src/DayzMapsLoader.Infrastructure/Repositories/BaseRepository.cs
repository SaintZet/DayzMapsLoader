using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity, new()
{
    private readonly DbContext _dbContext;

    public BaseRepository(DbContext dayzMapLoaderContext)
    {
        _dbContext = dayzMapLoaderContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        try
        {
            return _dbContext.Set<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't retrieve entities: {ex.Message}");
        }
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");

        if (entity.Id != 0 && await _dbContext.Set<TEntity>().AnyAsync(e => e.Id == entity.Id))
            throw new InvalidOperationException($"Entity with ID {entity.Id} already exists in the database");

        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");

        try
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
        }
    }
    public async Task DeleteAsync(TEntity entity)
    {
        if (entity is null)
            throw new ArgumentNullException($"{nameof(DeleteAsync)} entity must not be null");

        try
        {
            var existingEntity = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
            if (existingEntity != null)
            {
                _dbContext.Remove(existingEntity);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Entity with ID {entity.Id} does not exist in the database.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Entity could not be deleted: {ex.Message}");
        }
    }
}