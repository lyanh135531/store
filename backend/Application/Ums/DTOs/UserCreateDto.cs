using Application.Common;
using FluentValidation;

namespace Application.Ums.DTOs;

public class UserCreateDto
{
    public required string UserName { get; set; }
    public string? FullName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
}

public class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(20);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();

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