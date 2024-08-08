using Application.Business.DTOs.Products;
using Application.Core.Services;
using AutoMapper;
using Domain.Business.Entities;
using Domain.Business.Repositories;
using Domain.Core;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Business.Services.Products;

public class ProductService(
    IRepository<Product, Guid> repository,
    IDistributedCache distributedCache,
    IMapper mapper,
    IFileService fileService,
    IProductRepository productRepository)
    : AppServiceBase<Product, Guid, ProductListDto, ProductDetailDto, ProductCreateDto, ProductUpdateDto>(repository,
        distributedCache, mapper), IProductService
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