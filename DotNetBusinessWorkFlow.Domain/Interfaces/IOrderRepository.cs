using DotNetBusinessWorkflow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task SaveAsync();
}
