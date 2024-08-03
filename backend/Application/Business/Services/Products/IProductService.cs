using Application.Business.DTOs.Products;
using Application.Core.Services;

namespace Application.Business.Services.Products;

public interface
    IProductService : IAppServiceBase<Guid, ProductListDto, ProductDetailDto, ProductCreateDto, ProductUpdateDto>
{
}