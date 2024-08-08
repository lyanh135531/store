using Application.Core.DTOs;
using Application.Core.Extensions;
using AutoMapper;
using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Application.Core.Services;

public class
    AppServiceBase<TEntity, TKey, TListDto, TDetailDto, TCreateDto, TUpdateDto>(
        IRepository<TEntity, TKey> repository,
        IDistributedCache distributedCache,
        IMapper mapper)
    : IAppServiceBase<TKey, TListDto,
        TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : class, IEntity<TKey>
    where TCreateDto : class
    where TUpdateDto : class, IEntityDto<TKey>
    where TDetailDto : class
{
    protected readonly IRepository<TEntity, TKey> Repository = repository;

    private const string ListCacheKey = $"{nameof(TEntity)}ListCache";
    private const string DetailCacheKey = $"{nameof(TEntity)}DetailCache";

    public virtual async Task<PaginatedList<TListDto>> GetListAsync(PaginatedListQuery query,
        CancellationToken cancellationToken = default)
    {
        var entitiesCache = await distributedCache.GetStringAsync(ListCacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(entitiesCache))
        {
            return JsonConvert.DeserializeObject<PaginatedList<TListDto>>(entitiesCache);
        }

        var queryable = await Repository.GetQueryableAsync();
        queryable = queryable.ApplyPaginatedFilter(query);
        var total = await queryable.CountAsync(cancellationToken: cancellationToken);
        var entities = await queryable
            .ApplyPaginatedListQuery(query)
            .ToListAsync(cancellationToken);
        var result = mapper.Map<List<TEntity>, List<TListDto>>(entities);
        var paginatedList = new PaginatedList<TListDto>(result, total, query.Offset, query.Limit);
        var valueCache = JsonConvert.SerializeObject(paginatedList);
        await SetCaching(ListCacheKey, valueCache, cancellationToken);
        return paginatedList;
    }

    public virtual async Task<TDetailDto> GetDetailAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entityCache = await distributedCache.GetStringAsync(DetailCacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(entityCache))
        {
            return JsonConvert.DeserializeObject<TDetailDto>(entityCache);
        }

        var entity = await Repository.FindAsync(id, cancellationToken: cancellationToken);
        var result = mapper.Map<TEntity, TDetailDto>(entity);
        var valueCache = JsonConvert.SerializeObject(result);
        await SetCaching(DetailCacheKey, valueCache, cancellationToken);
        return result ?? throw new Exception("Not Found!");
    }

    public virtual async Task<TDetailDto> UpdateAsync(TUpdateDto updateDto)
    {
        var queryable = await Repository.GetQueryableAsync();
        var entity = await queryable.FirstOrDefaultAsync(x => x.Id.Equals(updateDto.Id));
        if (entity is null) throw new Exception("Not Found");

        mapper.Map(updateDto, entity);
        var entityNew = await Repository.UpdateAsync(entity, true);
        var result = mapper.Map<TEntity, TDetailDto>(entityNew);
        await RemoveCaching();
        return result;
    }

    public virtual async Task<TDetailDto> CreateAsync(TCreateDto createDto)
    {
        var entity = mapper.Map<TCreateDto, TEntity>(createDto);
        var entityNew = await Repository.AddAsync(entity, true);
        var result = mapper.Map<TEntity, TDetailDto>(entityNew);
        await RemoveCaching();
        return result;
    }

    public virtual async Task<TDetailDto> DeleteAsync(TKey id)
    {
        var entity = await Repository.DeleteAsync(id, true);
        var result = mapper.Map<TEntity, TDetailDto>(entity);
        await RemoveCaching();
        return result;
    }

    private async Task SetCaching(string cacheKey, string value, CancellationToken cancellationToken)
    {
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        await distributedCache.SetStringAsync(cacheKey, value, options, cancellationToken);
    }

    private async Task RemoveCaching()
    {
        await distributedCache.RemoveAsync(ListCacheKey);
        await distributedCache.RemoveAsync(DetailCacheKey);
    }
}