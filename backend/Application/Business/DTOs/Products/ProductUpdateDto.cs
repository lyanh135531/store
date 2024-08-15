using Domain.Core;
using FluentValidation;

namespace Application.Business.DTOs.Products;

public class ProductUpdateDto : IEntityDto<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }
}

public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
        
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