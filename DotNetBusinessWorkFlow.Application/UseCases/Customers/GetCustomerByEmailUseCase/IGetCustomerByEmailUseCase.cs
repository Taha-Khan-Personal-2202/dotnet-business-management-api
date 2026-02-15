using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByEmailUseCase;

public interface IGetCustomerByEmailUseCase
{
    Task<OperationResult<CustomerResponseDto>?> ExecuteAsync(string email);
}


