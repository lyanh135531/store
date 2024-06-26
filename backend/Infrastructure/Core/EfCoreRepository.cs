using Domain.Core;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Core;

public class EfCoreRepository<TEntity, TKey> : IEfCoreRepository<TEntity, TKey>, IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly ApplicationDbContext _applicationDbContext;

    protected EfCoreRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    private Task<DbSet<TEntity>> GetDbSetAsync()
    {
        return Task.FromResult(_applicationDbContext.Set<TEntity>());
    }

    public virtual async Task<TEntity> FindAsync(TKey id, bool isTracking = true,
        CancellationToken cancellationToken = default)
    {
        return await GetAsync(id, isTracking, cancellationToken) ??
               throw new Exception("Not found!");
    }

    public virtual async Task<TEntity> GetAsync(TKey id, bool isTracking = true,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetQueryableAsync();
        if (!isTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        var result = await queryable.FirstOrDefaultAsync(x => x.Id != null && x.Id.Equals(id), cancellationToken);
        if (result == null)
        {
            throw new Exception("Not found!");
        }

        return result;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, bool autoSave = false)
    {
        var saveEntity = _applicationDbContext.AddAsync(entity).Result.Entity;
        if (autoSave)
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        return saveEntity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave)
    {
        var updateEntity = _applicationDbContext.Update(entity).Entity;
        if (autoSave)
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        return updateEntity;
    }

    public virtual async Task<TEntity> DeleteAsync(TKey id, bool autoSave = false)
    {
        var entity = await FindAsync(id);
        _applicationDbContext.Remove(entity);
        if (autoSave)
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        return entity;
    }

    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        var queryable = (await GetDbSetAsync()).AsQueryable();
        return queryable;
    }
}