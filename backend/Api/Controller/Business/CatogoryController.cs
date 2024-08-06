using Api.Core;
using Application.Business.DTOs.Categories;
using Application.Business.DTOs.Products;
using Application.Business.Services.Categories;
using Application.Business.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Business;

[ApiController]
[Route("/api/admin/category")]
[Authorize]
public class CategoryController(ICategoryService categoryService)
    : ApiControllerBase<Guid, CategoryListDto, CategoryDetailDto, CategoryCreateDto, CategoryUpdateDto>(categoryService)
{
}