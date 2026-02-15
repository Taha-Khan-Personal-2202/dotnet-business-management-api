using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByIdUseCase;

public interface IGetCustomerByIdUseCase
{
    Task<OperationResult<CustomerResponseDto>> ExecuteAsync(Guid id);
}
