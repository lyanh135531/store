namespace Infrastructure.Core;

public interface IEfCoreRepository<TEntity, TKey>
{
    Task<IQueryable<TEntity>> GetQueryableAsync();

    Task<TEntity> FindAsync(TKey id, bool isTracking = true, CancellationToken cancellationToken = default);
    Task<TEntity> GetAsync(TKey id, bool isTracking = true, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, bool autoSave = false);
    Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false);
    Task<TEntity> DeleteAsync(TKey id, bool autoSave = false);
}