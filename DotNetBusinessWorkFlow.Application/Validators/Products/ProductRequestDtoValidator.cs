using DotNetBusinessWorkFlow.Application.DTOs.Products;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.Validators.Products;

public sealed class ProductRequestDtoValidator : AbstractValidator<ProductRequestDto>
{
    public ProductRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(150).WithMessage("Name cannot exceed 150 characters");

        RuleFor(x => x.Price.Amount)
            .GreaterThan(0).WithMessage("Price must be greater than zero");

        RuleFor(x => x.Price.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .Length(3).WithMessage("Currency must be a 3-letter code (e.g., INR)");
    }
}