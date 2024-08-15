using Application.Common;
using Domain.Core;
using FluentValidation;

namespace Application.Ums.DTOs;

public class UserUpdateDto : IEntityDto<Guid>
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
}

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Gender)
            .NotEmpty()
            .NotNull()
            .Must(ValidatorUtil.ValidGender);
    }
}