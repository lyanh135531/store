using Domain.Core;
using FluentValidation;

namespace Application.Business.DTOs.Categories;

public class CategoryCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}

public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100);
    }
}