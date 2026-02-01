using DotNetBusinessWorkFlow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Interfaces;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
    Task<Payment?> GetByOrderIdAsync(Guid orderId);
}
