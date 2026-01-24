using DotNetBusinessWorkflow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Domain.Interfaces;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
}
