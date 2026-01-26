using DotNetBusinessWorkflow.Domain.Repositories;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.Mappings;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.GetProductByIdUseCase;

public class GetProductByIdUseCase(IProductRepository repository) : IGetProductByIdUseCase
{
    private IProductRepository _repository { get; } = repository;

    public async Task<ProductResponseDto?> ExecuteAsync(Guid productId)
    {
        var product = await _repository.GetByIdAsync(productId);
        return EntityToDtoMapping.MapProduct(product);
    }
}
