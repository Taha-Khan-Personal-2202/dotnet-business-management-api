using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.GetAllProductsUseCase;

public sealed class GetAllProductsUseCase : IGetAllProductsUseCase
{
    private readonly IProductRepository _repository;

    public GetAllProductsUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<IEnumerable<ProductResponseDto>>> ExecuteAsync()
    {
        var products = await _repository.GetAllAsync();

        var dtos = products
            .Select(EntityToDtoMapping.MapProduct)
            .ToList();

        return OperationResult<IEnumerable<ProductResponseDto>>.Ok(dtos);
    }
}