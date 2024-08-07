using Application.Business.DTOs.Products;
using Application.Core.Services;
using AutoMapper;
using Domain.Business.Entities;
using Domain.Business.Repositories;

namespace Application.Business.Services.Products;

public class ProductService(IProductRepository productRepository, IMapper mapper, IFileService fileService) :
    AppServiceBase<Product, Guid, ProductListDto, ProductDetailDto, ProductCreateDto, ProductUpdateDto>(
        productRepository, mapper), IProductService
{
    private readonly IMapper _mapper = mapper;

    public override async Task<ProductDetailDto> CreateAsync(ProductCreateDto createDto)
    {
        var product = _mapper.Map<ProductCreateDto, Product>(createDto);
        product.Code = await GenerateCodeAsync();
        product.FileEntryCollectionId = await fileService.UploadMultiFileAsync(createDto.Files);
        var productNew = await Repository.AddAsync(product, true);
        var result = _mapper.Map<Product, ProductDetailDto>(productNew);
        return result;
    }

    private async Task<string> GenerateCodeAsync()
    {
        var sequenceNumber = await productRepository.GetNextSequenceNumberAsync();
        return $"P{sequenceNumber:D4}";
    }
}