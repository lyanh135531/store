using FluentValidation;

namespace Api.DTOs;

public class LoginModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}

public class LoginValidator : AbstractValidator<LoginModel>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}