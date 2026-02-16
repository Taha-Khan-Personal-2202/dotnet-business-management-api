using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;

public sealed class DeactivateProductUseCase : IDeactivateProductUseCase
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateProductUseCase(
        IProductRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<ProductResponseDto>> ExecuteAsync(Guid productId)
    {
        var product = await _repository.GetByIdAsync(productId);
        if (product is null)
        {
            return OperationResult<ProductResponseDto>.Error("Product not found.", 404);
        }

        product.Deactivate();

        await _repository.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = EntityToDtoMapping.MapProduct(product);
        return OperationResult<ProductResponseDto>.Ok(responseDto, "Product deactivated successfully.");
    }
}