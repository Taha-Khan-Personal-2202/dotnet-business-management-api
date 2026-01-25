using DotNetBusinessWorkflow.Domain.Repositories;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;

public sealed class UpdateProductUseCase(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork) : IUpdateProductUseCase
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(ProductRequestUpdateDto request)
    {
        var product = await _productRepository.GetByIdAsync(request.Id)
            ?? throw new InvalidOperationException("Product not found.");

        await _productRepository.UpdateAsync(product);
       
        await _unitOfWork.SaveChangesAsync();
    }
}
