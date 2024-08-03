using Application.Business.DTOs.Products;
using Application.Core.Services;
using AutoMapper;
using Domain.Business.Entities;
using Domain.Business.Repositories;

namespace Application.Business.Services.Products;

public class ProductService :
    AppServiceBase<Product, Guid, ProductListDto, ProductDetailDto, ProductCreateDto, ProductUpdateDto>, IProductService
{
    public ProductService(IProductRepository productRepository, IMapper mapper) : base(productRepository, mapper)
    {
    }
}