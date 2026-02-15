using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.DTOs.Common;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;

public interface IUpdateCustomerUseCase
{
    Task<OperationResult<bool>> ExecuteAsync(Guid customerId, CustomerRequestUpdateDto request);
}
