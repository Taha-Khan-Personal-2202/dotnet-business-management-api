using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;

public interface IUpdateProductUseCase
{
    Task<OperationResult<ProductResponseDto>> ExecuteAsync(ProductRequestUpdateDto dto);
}