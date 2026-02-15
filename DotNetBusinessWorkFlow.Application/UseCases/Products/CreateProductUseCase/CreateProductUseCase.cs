using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.CreateProductUseCase;

public class CreateProductUseCase(IProductRepository repository,
    IUnitOfWork unitOfWork) : ICreateProductUseCase
{
    private IProductRepository _repository { get; } = repository;
    private IUnitOfWork _unitOfWork { get; } = unitOfWork;

    public async Task<OperationResult<Guid>> ExecuteAsync(ProductRequestDto request)
    {
        var product = new Product(request.Name, request.Price);
        await _repository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult<Guid>.Succces(product.Id, "Product created successfully.", 201);
    }
}
