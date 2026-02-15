using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;

public interface IUpdateCustomerUseCase
{
    Task<OperationResult<CustomerResponseDto>> ExecuteAsync(Guid customerId, CustomerRequestUpdateDto dto);
}