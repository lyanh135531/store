using Api.Core;
using Application.Business.DTOs.Products;
using Application.Business.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Business;

[ApiController]
[Route("/api/admin/product")]
public class
    ProductController : ApiControllerBase<Guid, ProductListDto, ProductDetailDto, ProductCreateDto, ProductUpdateDto>
{
    private readonly IProductService _userService;

    public ProductController(IProductService userService) : base(userService)
    {
        _userService = userService;
    }
}