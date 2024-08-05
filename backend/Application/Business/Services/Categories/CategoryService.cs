using Application.Business.DTOs.Categories;
using Application.Core.Services;
using AutoMapper;
using Domain.Business.Entities;
using Domain.Business.Repositories;

namespace Application.Business.Services.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) :
    AppServiceBase<Category, Guid, CategoryListDto, CategoryDetailDto, CategoryCreateDto, CategoryUpdateDto>(
        categoryRepository, mapper), ICategoryService;