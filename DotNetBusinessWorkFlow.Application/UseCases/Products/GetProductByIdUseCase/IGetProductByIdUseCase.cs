using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.GetProductByIdUseCase;

public interface IGetProductByIdUseCase
{
    Task<ProductResponseDto?> ExecuteAsync(Guid productId);
}
