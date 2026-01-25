using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.GetAllProductsUseCase;

public interface IGetAllProductsUseCase
{
    Task<IReadOnlyList<ProductResponseDto>> ExecuteAsync();
}
