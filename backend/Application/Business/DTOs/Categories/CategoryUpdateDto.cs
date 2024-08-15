using Domain.Core;

namespace Application.Business.DTOs.Categories;

public class CategoryUpdateDto : EntityDto<Guid>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}