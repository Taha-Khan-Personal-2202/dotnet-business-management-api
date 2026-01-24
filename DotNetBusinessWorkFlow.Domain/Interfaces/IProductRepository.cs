using DotNetBusinessWorkflow.Domain.Entities;

namespace DotNetBusinessWorkflow.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
}
