using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.Validators.Products;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Repositories;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.CreateProductUseCase;

public sealed class CreateProductUseCase : ICreateProductUseCase
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ProductRequestDtoValidator _validator;

    public CreateProductUseCase(
        IProductRepository repository,
        IUnitOfWork unitOfWork,
        ProductRequestDtoValidator validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<OperationResult<Guid>> ExecuteAsync(ProductRequestDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<Guid>.Error($"Validation failed: {errors}", 400);
        }

        var product = new Product(dto.Name, dto.Price);

        await _repository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<Guid>.Ok(product.Id, "Product created successfully.", 201);
    }
}