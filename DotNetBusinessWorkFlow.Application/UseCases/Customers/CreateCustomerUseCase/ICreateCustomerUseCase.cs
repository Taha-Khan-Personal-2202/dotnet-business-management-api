using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;

public interface ICreateCustomerUseCase
{
    Task<OperationResult<Guid>> ExecuteAsync(CustomerRequestDto dto);
}