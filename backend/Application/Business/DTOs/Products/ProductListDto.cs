using Domain.Core;

namespace Application.Business.DTOs.Products;

public class ProductListDto : IEntityDto<Guid>
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    public Guid CategoryId { get; set; }
}