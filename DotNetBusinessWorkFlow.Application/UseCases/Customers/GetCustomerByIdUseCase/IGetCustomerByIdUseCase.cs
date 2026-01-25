using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByIdUseCase;

public interface IGetCustomerByIdUseCase
{
    Task<CustomerResponseDto?> ExecuteAsync(Guid id);
}
