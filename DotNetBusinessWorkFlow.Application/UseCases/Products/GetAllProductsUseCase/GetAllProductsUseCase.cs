using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.GetAllProductsUseCase;

public class GetAllProductsUseCase(IProductRepository repository) : IGetAllProductsUseCase
{
    private readonly IProductRepository _repository = repository;
    public async Task<OperationResult<IReadOnlyList<ProductResponseDto>>> ExecuteAsync()
    {
        var products = await _repository.GetAllAsync();
        var result = products != null && products.Any() ? products.Select(EntityToDtoMapping.MapProduct).ToList() : new List<ProductResponseDto>();
        return OperationResult<IReadOnlyList<ProductResponseDto>>.Succces(result, "Products fetched successfully.");
    }
}
