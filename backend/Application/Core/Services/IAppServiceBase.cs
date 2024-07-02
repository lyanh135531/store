using Application.Core.DTOs;

namespace Application.Core.Services;

public interface IAppServiceBase<in TKey, TListDto, TDetailDto, in TCreateDto, in TUpdateDto> where TDetailDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    Task<PaginatedList<TListDto>> GetListAsync(PaginatedListQuery query, CancellationToken cancellationToken = default);

    Task<TDetailDto> GetDetailAsync(TKey id, CancellationToken cancellationToken = default);

    Task<TDetailDto> UpdateAsync(TUpdateDto updateDto);

    Task<TDetailDto> CreateAsync(TCreateDto createDto);

    Task<TDetailDto> DeleteAsync(TKey id);
}