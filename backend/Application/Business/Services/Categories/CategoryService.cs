using Application.Business.DTOs.Categories;
using Application.Core.Services;
using AutoMapper;
using Domain.Business.Entities;
using Domain.Core;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Business.Services.Categories;

public class CategoryService
    : AppServiceBase<Category, Guid, CategoryListDto, CategoryDetailDto, CategoryCreateDto, CategoryUpdateDto>,
        ICategoryService
{
    public CategoryService(IRepository<Category, Guid> repository, IDistributedCache distributedCache, IMapper mapper) :
        base(repository, distributedCache, mapper)
    {
    }
}