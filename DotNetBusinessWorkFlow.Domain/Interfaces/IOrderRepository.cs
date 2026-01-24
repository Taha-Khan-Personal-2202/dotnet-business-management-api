using DotNetBusinessWorkflow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}
