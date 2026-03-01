using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.Validators.Customers;

public sealed class CustomerRequestUpdateDtoValidator : AbstractValidator<CustomerRequestUpdateDto>
{
    public CustomerRequestUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please provide a valid email address.")
            .MaximumLength(150).WithMessage("Email cannot exceed 150 characters.");
    }
}