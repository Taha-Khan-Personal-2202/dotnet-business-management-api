using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.CreateProductUseCase;

public interface ICreateProductUseCase
{
    Task<Guid> ExecuteAsync(ProductRequestDto request);
}
