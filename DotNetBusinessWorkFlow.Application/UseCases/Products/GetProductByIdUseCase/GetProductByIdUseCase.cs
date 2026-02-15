using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.GetProductByIdUseCase;

public class GetProductByIdUseCase(IProductRepository repository) : IGetProductByIdUseCase
{
    private IProductRepository _repository { get; } = repository;

    public async Task<OperationResult<ProductResponseDto>> ExecuteAsync(Guid productId)
    {
        var product = EntityToDtoMapping.MapProduct(await _repository.GetByIdAsync(productId));
        return product is null
            ? OperationResult<ProductResponseDto>.Fail("Product not found.", 404)
            : OperationResult<ProductResponseDto>.Succces(product, "Product fetched successfully.");
    }
}
