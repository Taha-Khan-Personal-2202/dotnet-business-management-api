using DotNetBusinessWorkflow.Domain.Repositories;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.Mappings;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.GetAllProductsUseCase;

public class GetAllProductsUseCase(IProductRepository repository) : IGetAllProductsUseCase
{
    private readonly IProductRepository _repository = repository;
    public async Task<IReadOnlyList<ProductResponseDto>> ExecuteAsync()
    {
        var products = await _repository.GetAllAsync();
        return products != null && products.Any() ? products.Select(EntityToDtoMapping.MapProduct).ToList() : new();
    }
}
