using Application.Business.DTOs.Categories;
using Application.Core.Services;
using AutoMapper;
using Domain.Business.Entities;
using Domain.Business.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Business.Services.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IDistributedCache distributedCache, IMapper mapper)
    : AppServiceBase<Category, Guid, CategoryListDto, CategoryDetailDto, CategoryCreateDto, CategoryUpdateDto>(
            categoryRepository, distributedCache, mapper),
        ICategoryService;