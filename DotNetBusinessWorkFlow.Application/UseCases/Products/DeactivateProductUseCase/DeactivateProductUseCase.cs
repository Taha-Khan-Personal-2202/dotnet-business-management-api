using DotNetBusinessWorkFlow.Domain.Repositories;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;

public class DeactivateProductUseCase(IProductRepository repository,
    IUnitOfWork unitOfWork) : IDeactivateProductUseCase
{
    public IProductRepository _repository { get; } = repository;
    public IUnitOfWork _unitOfWork { get; } = unitOfWork;

    public async Task ExecuteAsync(Guid productId)
    {
        await _repository.DeActivateAsync(productId);
        await _unitOfWork.SaveChangesAsync();
    }
}
