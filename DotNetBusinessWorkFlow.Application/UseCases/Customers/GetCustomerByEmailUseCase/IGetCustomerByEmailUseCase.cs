using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByEmailUseCase;

public interface IGetCustomerByEmailUseCase
{
    Task<CustomerResponseDto?> ExecuteAsync(string email);
}
