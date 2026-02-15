using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.DeactivateCustomerUseCase;

public interface IDeactivateCustomerUseCase
{
    Task<OperationResult<CustomerResponseDto>> ExecuteAsync(Guid customerId);
}