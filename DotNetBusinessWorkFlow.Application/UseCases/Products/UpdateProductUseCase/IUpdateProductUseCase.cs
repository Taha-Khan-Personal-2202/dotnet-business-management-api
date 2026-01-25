using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;

public interface IUpdateProductUseCase
{
    Task ExecuteAsync(ProductRequestUpdateDto request);
}
