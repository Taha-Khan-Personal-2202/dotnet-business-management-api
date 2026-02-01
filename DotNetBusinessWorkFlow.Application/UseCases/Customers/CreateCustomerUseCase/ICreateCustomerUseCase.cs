using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;

public interface ICreateCustomerUseCase
{
    Task<Guid> ExecuteAsync(CustomerRequestDto customer, CancellationToken cancellationToken = default);
}
