using Domain.Core;

namespace Application.Business.DTOs.Categories;

public class CategoryCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}