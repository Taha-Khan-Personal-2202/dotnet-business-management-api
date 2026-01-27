using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;

public interface IUpdateCustomerUseCase
{
    Task ExecuteAsync(Guid customerId, CustomerRequestUpdateDto request);
}
