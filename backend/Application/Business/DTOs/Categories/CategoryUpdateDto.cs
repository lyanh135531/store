using Domain.Core;
using FluentValidation;

namespace Application.Business.DTOs.Categories;

public class CategoryUpdateDto : EntityDto<Guid>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}

public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100);
    }
}