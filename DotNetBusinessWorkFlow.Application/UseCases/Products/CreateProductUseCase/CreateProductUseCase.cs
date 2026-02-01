using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Repositories;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.CreateProductUseCase;

public class CreateProductUseCase(IProductRepository repository,
    IUnitOfWork unitOfWork) : ICreateProductUseCase
{   
    private IProductRepository _repository { get; } = repository;
    private IUnitOfWork _unitOfWork { get; } = unitOfWork;

    public async Task<Guid> ExecuteAsync(ProductRequestDto request)
    {
        var product = new Product(request.Name, request.Price);
        await _repository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return product.Id;
    }
}
