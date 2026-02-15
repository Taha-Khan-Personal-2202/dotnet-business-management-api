using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.CreateProductUseCase;

public interface ICreateProductUseCase
{
    Task<OperationResult<Guid>> ExecuteAsync(ProductRequestDto request);
}
