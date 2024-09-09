using Application.Core.DTOs;
using Application.Core.Extensions;
using AutoMapper;
using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Application.Core.Services;

public class AppServiceBase<TEntity, TKey, TListDto, TDetailDto, TCreateDto, TUpdateDto>(
    IRepository<TEntity, TKey> repository,
    IDistributedCache distributedCache,
    IMapper mapper)
    : IAppServiceBase<TKey, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : class, IEntity<TKey>
    where TCreateDto : class
    where TUpdateDto : class, IEntityDto<TKey>
    where TDetailDto : class
{
    protected readonly IRepository<TEntity, TKey> Repository = repository;
    private readonly CacheService _cacheService = new(distributedCache);

    public virtual async Task<PaginatedList<TListDto>> GetListAsync(
        PaginatedListQuery query,
        CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{typeof(TEntity).Name}ListCache";
        var cachedList = await _cacheService.GetCachedDataAsync<PaginatedList<TListDto>>(cacheKey, cancellationToken)
            .ConfigureAwait(false);

        if (cachedList is not null)
            return cachedList;

        var queryable = await Repository.GetQueryableAsync().ConfigureAwait(false);
        queryable = queryable.ApplyPaginatedFilter(query);
        var total = await queryable.CountAsync(cancellationToken).ConfigureAwait(false);
        var entities = await queryable
            .ApplyPaginatedListQuery(query)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var result = mapper.Map<List<TEntity>, List<TListDto>>(entities);
        var paginatedList = new PaginatedList<TListDto>(result, total, query.Offset, query.Limit);

        await _cacheService.SetCacheAsync(cacheKey, paginatedList, cancellationToken).ConfigureAwait(false);
        return paginatedList;
    }

    public virtual async Task<TDetailDto> GetDetailAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{typeof(TEntity).Name}{id}DetailCache";
        var cachedDetail = await _cacheService.GetCachedDataAsync<TDetailDto>(cacheKey, cancellationToken)
            .ConfigureAwait(false);

        if (cachedDetail is not null)
            return cachedDetail;

        var entity = await Repository.FindAsync(id, cancellationToken: cancellationToken).ConfigureAwait(false);
        if (entity is null)
            throw new KeyNotFoundException($"Entity with id {id} not found.");

        var result = mapper.Map<TEntity, TDetailDto>(entity);
        await _cacheService.SetCacheAsync(cacheKey, result, cancellationToken).ConfigureAwait(false);
        return result;
    }

    public virtual async Task<TDetailDto> UpdateAsync(TUpdateDto updateDto)
    {
        var queryable = await Repository.GetQueryableAsync().ConfigureAwait(false);
        var entity = await queryable.FirstOrDefaultAsync(x => x.Id != null && x.Id.Equals(updateDto.Id))
            .ConfigureAwait(false);

        if (entity is null)
            throw new KeyNotFoundException($"Entity with id {updateDto.Id} not found.");

        mapper.Map(updateDto, entity);
        var updatedEntity = await Repository.UpdateAsync(entity, true).ConfigureAwait(false);
        var result = mapper.Map<TEntity, TDetailDto>(updatedEntity);
        await InvalidateCacheAsync(updateDto.Id).ConfigureAwait(false);
        return result;
    }

    public virtual async Task<TDetailDto> CreateAsync(TCreateDto createDto)
    {
        var entity = mapper.Map<TCreateDto, TEntity>(createDto);
        var createdEntity = await Repository.AddAsync(entity, true).ConfigureAwait(false);
        var result = mapper.Map<TEntity, TDetailDto>(createdEntity);
        await InvalidateCacheAsync(default!).ConfigureAwait(false);
        return result;
    }

    public virtual async Task<TDetailDto> DeleteAsync(TKey id)
    {
        var entity = await Repository.DeleteAsync(id, true).ConfigureAwait(false);
        var result = mapper.Map<TEntity, TDetailDto>(entity);
        await InvalidateCacheAsync(id).ConfigureAwait(false);
        return result;
    }

    private async Task InvalidateCacheAsync(TKey id)
    {
        await _cacheService.RemoveCacheAsync($"{typeof(TEntity).Name}ListCache").ConfigureAwait(false);
        await _cacheService.RemoveCacheAsync($"{typeof(TEntity).Name}{id}DetailCache").ConfigureAwait(false);
    }

    private class CacheService(IDistributedCache distributedCache)
    {
        public async Task SetCacheAsync<T>(string cacheKey, T value, CancellationToken cancellationToken)
        {
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            var serializedValue = JsonConvert.SerializeObject(value);
            await distributedCache.SetStringAsync(cacheKey, serializedValue, options, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<T?> GetCachedDataAsync<T>(string cacheKey, CancellationToken cancellationToken)
        {
            var cachedData = await distributedCache.GetStringAsync(cacheKey, cancellationToken)
                .ConfigureAwait(false);
            return string.IsNullOrEmpty(cachedData) ? default : JsonConvert.DeserializeObject<T>(cachedData);
        }

        public async Task RemoveCacheAsync(string cacheKey)
        {
            await distributedCache.RemoveAsync(cacheKey).ConfigureAwait(false);
        }
    }
}