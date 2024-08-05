using Domain.Core;

namespace Application.Business.DTOs.Categories;

public class CategoryUpdateDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
}