using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;

public sealed class UpdateProductUseCase : IUpdateProductUseCase
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ProductRequestUpdateDto> _validator;

    public UpdateProductUseCase(
        IProductRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<ProductRequestUpdateDto> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<OperationResult<ProductResponseDto>> ExecuteAsync(ProductRequestUpdateDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<ProductResponseDto>.Error(errors, 400);
        }

        var product = await _repository.GetByIdAsync(dto.Id);
        if (product is null)
        {
            return OperationResult<ProductResponseDto>.Error("Product not found.", 404);
        }

        product.Update(dto.Name, dto.Price);

        await _repository.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = EntityToDtoMapping.MapProduct(product);
        return OperationResult<ProductResponseDto>.Ok(responseDto, "Product updated successfully.");
    }
}