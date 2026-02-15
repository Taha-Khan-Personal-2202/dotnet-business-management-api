using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;

public sealed class UpdateProductUseCase(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IUpdateProductUseCase
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OperationResult<bool>> ExecuteAsync(ProductRequestUpdateDto request)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
        {
            return OperationResult<bool>.Fail("Product not found.", 404);
        }

        product.Update(request.Name, request.Price);

        await _productRepository.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult<bool>.Succces(true, "Product updated successfully.");
    }
}
