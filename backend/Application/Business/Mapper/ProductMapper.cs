using Application.Business.DTOs.Products;
using AutoMapper;
using Domain.Business.Entities;

namespace Application.Business.Mapper;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductListDto>();
        CreateMap<Product, ProductDetailDto>();

        CreateMap<ProductUpdateDto, Product>();
        CreateMap<ProductCreateDto, Product>();
    }
}