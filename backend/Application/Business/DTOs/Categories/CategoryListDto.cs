using Domain.Core;

namespace Application.Business.DTOs.Categories;

public class CategoryListDto : EntityDto<Guid>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}