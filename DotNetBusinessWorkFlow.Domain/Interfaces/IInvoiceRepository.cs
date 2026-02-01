using DotNetBusinessWorkFlow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Repositories;

public interface IInvoiceRepository
{
    Task AddAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(Guid id);
    Task<Invoice?> GetByOrderIdAsync(Guid orderId);
    Task<IEnumerable<Invoice>> GetAllAsync();
}
