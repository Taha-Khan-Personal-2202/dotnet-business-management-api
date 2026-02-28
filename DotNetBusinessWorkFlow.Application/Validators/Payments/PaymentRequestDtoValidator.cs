using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.Validators.Payments;

public sealed class PaymentRequestDtoValidator : AbstractValidator<PaymentRequestDto>
{
    public PaymentRequestDtoValidator()
    {

        RuleFor(x => x.Amount.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero");

        RuleFor(x => x.Amount.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .Length(3).WithMessage("Currency must be a 3-letter code (e.g., INR)");
    }

}
