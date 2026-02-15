using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;

public interface IUpdateProductUseCase
{
    Task<OperationResult<bool>> ExecuteAsync(ProductRequestUpdateDto request);
}
