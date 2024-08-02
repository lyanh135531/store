using Application.Core.DTOs;
using AutoMapper;
using Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Services;

public class
    AppServiceBase<TEntity, TKey, TListDto, TDetailDto, TCreateDto, TUpdateDto> : IAppServiceBase<TKey, TListDto,
        TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : class, IEntity<TKey>
    where TCreateDto : class
    where TUpdateDto : class, IEntityDto<TKey>
    where TDetailDto : class
{
    protected readonly IRepository<TEntity, TKey> Repository;
    private readonly IMapper _mapper;

    protected AppServiceBase(IRepository<TEntity, TKey> repository, IMapper mapper)
    {
        Repository = repository;
        _mapper = mapper;
    }


    public virtual async Task<PaginatedList<TListDto>> GetListAsync(PaginatedListQuery query,
        CancellationToken cancellationToken = default)
    {
        var queryable = await Repository.GetQueryableAsync();
        var total = await queryable.CountAsync(cancellationToken: cancellationToken);
        var entities = await queryable.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<TEntity>, List<TListDto>>(entities);
        return new PaginatedList<TListDto>(result, total, query.Offset, query.Limit);
    }

    public virtual async Task<TDetailDto> GetDetailAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.FindAsync(id, cancellationToken: cancellationToken);
        var result = _mapper.Map<TEntity, TDetailDto>(entity);
        return result ?? throw new Exception("Not Found!");
    }

    public virtual async Task<TDetailDto> UpdateAsync(TUpdateDto updateDto)
    {
        var queryable = await Repository.GetQueryableAsync();
        var entity = await queryable.FirstOrDefaultAsync(x => x.Id.Equals(updateDto.Id));
        if (entity is null) throw new Exception("Not Found");

        _mapper.Map(updateDto, entity);
        var entityNew = await Repository.UpdateAsync(entity, true);
        var result = _mapper.Map<TEntity, TDetailDto>(entityNew);
        return result;
    }

    public virtual async Task<TDetailDto> CreateAsync(TCreateDto createDto)
    {
        var entity = _mapper.Map<TCreateDto, TEntity>(createDto);
        var entityNew = await Repository.AddAsync(entity, true);
        var result = _mapper.Map<TEntity, TDetailDto>(entityNew);
        return result;
    }

    public virtual async Task<TDetailDto> DeleteAsync(TKey id)
    {
        var entity = await Repository.DeleteAsync(id, true);
        var result = _mapper.Map<TEntity, TDetailDto>(entity);
        return result;
    }
}