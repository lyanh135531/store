using Application.Core.DTOs;
using Application.Core.Services;
using Domain.Core;
using Microsoft.AspNetCore.Mvc;

namespace Api.Core;

public abstract class ApiControllerBase<TKey, TListDto, TDetailDto, TCreateDto, TUpdateDto>(
        IAppServiceBase<TKey, TListDto, TDetailDto, TCreateDto, TUpdateDto> appServiceBase)
    : ControllerBase where TDetailDto : class
    where TCreateDto : class
    where TUpdateDto : class, IEntityDto<TKey>
{
    [HttpGet("list")]
    public virtual async Task<ApiResponse<PaginatedList<TListDto>>> GetListAsync([FromQuery] PaginatedListQuery query,
        CancellationToken cancellationToken = default)
    {
        var listResult = await appServiceBase.GetListAsync(query, cancellationToken);
        return ApiResponse<PaginatedList<TListDto>>.Ok(listResult);
    }

    [HttpGet("detail")]
    public virtual async Task<ApiResponse<TDetailDto>> GetDetailAsync(TKey id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await appServiceBase.GetDetailAsync(id, cancellationToken);
            return ApiResponse<TDetailDto>.Ok(result);
        }
        catch (Exception e)
        {
            return ApiResponse<TDetailDto>.Fail(e.Message);
        }
    }

    [HttpPost("create")]
    public virtual async Task<ApiResponse<TDetailDto>> CreateAsync([FromBody] TCreateDto createDto)
    {
        try
        {
            var result = await appServiceBase.CreateAsync(createDto);
            return ApiResponse<TDetailDto>.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ApiResponse<TDetailDto>.Fail(e.Message);
        }
    }

    [HttpPut("update")]
    public virtual async Task<ApiResponse<TDetailDto>> UpdateAsync([FromBody] TUpdateDto updateDto)
    {
        try
        {
            var result = await appServiceBase.UpdateAsync(updateDto);
            return ApiResponse<TDetailDto>.Ok(result);
        }
        catch (Exception e)
        {
            return ApiResponse<TDetailDto>.Fail(e.Message);
        }
    }

    [HttpDelete("delete")]
    public virtual async Task<ApiResponse<TDetailDto>> DeleteAsync(TKey id)
    {
        try
        {
            var result = await appServiceBase.DeleteAsync(id);
            return ApiResponse<TDetailDto>.Ok(result);
        }
        catch (Exception e)
        {
            return ApiResponse<TDetailDto>.Fail(e.Message);
        }
    }
}