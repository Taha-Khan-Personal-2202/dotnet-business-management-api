using DotNetBusinessWorkflow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Interfaces;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
}
