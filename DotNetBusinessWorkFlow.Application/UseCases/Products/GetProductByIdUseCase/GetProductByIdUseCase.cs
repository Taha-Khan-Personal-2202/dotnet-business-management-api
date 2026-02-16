using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.GetProductByIdUseCase;

public sealed class GetProductByIdUseCase : IGetProductByIdUseCase
{
    private readonly IProductRepository _repository;

    public GetProductByIdUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<ProductResponseDto?>> ExecuteAsync(Guid productId)
    {
        var product = await _repository.GetByIdAsync(productId);
        if (product is null)
        {
            return OperationResult<ProductResponseDto?>.Error("Product not found.", 404, null);
        }

        var dto = EntityToDtoMapping.MapProduct(product);
        return OperationResult<ProductResponseDto?>.Ok(dto);
    }
}