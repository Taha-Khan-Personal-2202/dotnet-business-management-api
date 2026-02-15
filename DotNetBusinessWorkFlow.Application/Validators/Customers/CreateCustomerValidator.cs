using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.Validators.Customers;

public class CreateCustomerValidator : AbstractValidator<CustomerRequestDto>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name can not be null or empty. ")
            .MaximumLength(50).WithMessage("The can not be greather then 50. ");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email can not be null or emapty. ")
            .EmailAddress().WithMessage("Invalid email address. ");
    }
}
