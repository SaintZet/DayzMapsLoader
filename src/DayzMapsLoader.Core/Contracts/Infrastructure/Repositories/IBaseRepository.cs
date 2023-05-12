namespace DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class, new()
{
    IQueryable<TEntity> GetAll();

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);
}