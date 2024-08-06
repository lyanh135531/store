using Api.Core;
using Application.Business.DTOs.Products;
using Application.Business.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Business;

[ApiController]
[Route("/api/admin/product")]
[Authorize]
public class
    ProductController(IProductService productService)
    : ApiControllerBase<Guid, ProductListDto, ProductDetailDto, ProductCreateDto, ProductUpdateDto>(productService)
{
}