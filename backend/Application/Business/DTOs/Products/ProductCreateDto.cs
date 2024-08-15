using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Business.DTOs.Products;

public class ProductCreateDto
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }

    public List<IFormFile> Files { get; set; } = [];
}

public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100);
        
        RuleFor(x => x.CategoryId)
            .NotNull();
        
        RuleFor(x => x.Price)
            .NotNull();
    }
}