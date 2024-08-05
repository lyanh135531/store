using Application.Business.DTOs.Categories;
using Application.Core.Services;

namespace Application.Business.Services.Categories;

public interface ICategoryService : IAppServiceBase<Guid, CategoryListDto, CategoryDetailDto, CategoryCreateDto,
    CategoryUpdateDto>
{
}