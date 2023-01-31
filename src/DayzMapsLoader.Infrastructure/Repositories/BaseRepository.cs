using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Infrastructure.Contexts;

namespace DayzMapsLoader.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DayzMapLoaderContext _dayzMapLoaderContext;

        public BaseRepository(DayzMapLoaderContext dayzMapLoaderContext)
        {
            _dayzMapLoaderContext = dayzMapLoaderContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _dayzMapLoaderContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await _dayzMapLoaderContext.AddAsync(entity);
                await _dayzMapLoaderContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                _dayzMapLoaderContext.Update(entity);
                await _dayzMapLoaderContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}