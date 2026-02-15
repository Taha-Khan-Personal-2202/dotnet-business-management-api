using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.DTOs.Common;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;

public interface ICreateCustomerUseCase
{
    Task<OperationResult<Guid>> ExecuteAsync(CustomerRequestDto customer, CancellationToken cancellationToken = default);
}
