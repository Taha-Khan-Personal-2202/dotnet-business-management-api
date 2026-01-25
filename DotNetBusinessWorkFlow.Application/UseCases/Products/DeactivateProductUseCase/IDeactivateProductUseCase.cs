namespace DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;

public interface IDeactivateProductUseCase
{
    Task ExecuteAsync(Guid productId);
}
