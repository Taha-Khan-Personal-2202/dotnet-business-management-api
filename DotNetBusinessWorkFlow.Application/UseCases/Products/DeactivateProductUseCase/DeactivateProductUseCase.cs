using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;

public class DeactivateProductUseCase(IProductRepository repository,
    IUnitOfWork unitOfWork) : IDeactivateProductUseCase
{
    public IProductRepository _repository { get; } = repository;
    public IUnitOfWork _unitOfWork { get; } = unitOfWork;

    public async Task<OperationResult<bool>> ExecuteAsync(Guid productId)
    {
        var product = await _repository.GetByIdAsync(productId);
        if (product is null)
        {
            return OperationResult<bool>.Fail("Product not found.", 404);
        }

        await _repository.DeActivateAsync(productId);
        await _unitOfWork.SaveChangesAsync();
        return OperationResult<bool>.Succces(true, "Product deactivated successfully.");
    }
}
