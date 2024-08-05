using Api.Core;
using Application.Business.DTOs.Categories;
using Application.Business.DTOs.Products;
using Application.Business.Services.Categories;
using Application.Business.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Business;

[ApiController]
[Route("/api/admin/category")]
public class
    CategoryController : ApiControllerBase<Guid, CategoryListDto, CategoryDetailDto, CategoryCreateDto,
    CategoryUpdateDto>
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService) : base(categoryService)
    {
        _categoryService = categoryService;
    }
}