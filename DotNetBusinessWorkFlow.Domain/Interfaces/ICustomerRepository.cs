using DotNetBusinessWorkflow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Interfaces;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
}
