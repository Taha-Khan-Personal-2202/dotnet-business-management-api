using DotNetBusinessWorkFlow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Interfaces;
public interface IInvoiceRepository
{
    Task AddAsync(Invoice invoice);
    Task<Invoice?> GetByOrderIdAsync(Guid orderId);
}
