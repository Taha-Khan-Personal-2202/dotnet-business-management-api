using DotNetBusinessWorkFlow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer?> GetByEmailAsync(string email);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task<IEnumerable<Customer>> GetAllAsync();
}
