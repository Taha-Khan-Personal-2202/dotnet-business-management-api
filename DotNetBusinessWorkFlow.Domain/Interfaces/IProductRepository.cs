using DotNetBusinessWorkFlow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeActivateAsync(Guid productId);
}
