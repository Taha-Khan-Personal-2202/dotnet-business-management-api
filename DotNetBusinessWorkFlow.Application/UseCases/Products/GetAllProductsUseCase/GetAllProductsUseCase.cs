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
        var returnList = new List<ProductResponseDto>();
        foreach (var product in products)
        {
            returnList.Add(EntityToDtoMapping.MapProductEntityToDto(product));
        }
        return returnList;
    }
}
