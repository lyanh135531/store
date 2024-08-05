using Application.Business.DTOs.Categories;
using AutoMapper;
using Domain.Business.Entities;

namespace Application.Business.Mapper;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<Category, CategoryListDto>();
        CreateMap<Category, CategoryDetailDto>();

        CreateMap<CategoryUpdateDto, Category>();
        CreateMap<CategoryCreateDto, Category>();
    }
}