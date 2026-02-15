using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.Validators.Auth;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email can not be null or empty.")
            .EmailAddress().WithMessage("Invalid Email address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password can not be null or empty.");
    }
}
