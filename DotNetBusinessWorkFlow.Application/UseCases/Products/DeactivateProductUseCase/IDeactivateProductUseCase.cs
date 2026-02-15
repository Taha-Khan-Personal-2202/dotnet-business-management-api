using DotNetBusinessWorkFlow.Application.DTOs.Common;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;

public interface IDeactivateProductUseCase
{
    Task<OperationResult<bool>> ExecuteAsync(Guid productId);
}
