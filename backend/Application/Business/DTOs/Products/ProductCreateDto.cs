using Microsoft.AspNetCore.Http;

namespace Application.Business.DTOs.Products;

public class ProductCreateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public List<IFormFile> Files { get; set; }
}