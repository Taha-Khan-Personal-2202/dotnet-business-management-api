using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;

public interface IDeactivateProductUseCase
{
    Task<OperationResult<ProductResponseDto>> ExecuteAsync(Guid productId);
}