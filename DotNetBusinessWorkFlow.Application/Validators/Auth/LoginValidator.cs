using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.Validators.Auth;

public sealed class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(120).WithMessage("Email cannot exceed 120 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }
}