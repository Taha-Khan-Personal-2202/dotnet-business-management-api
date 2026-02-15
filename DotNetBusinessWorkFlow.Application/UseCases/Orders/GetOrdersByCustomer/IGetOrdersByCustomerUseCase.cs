using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrdersByCustomer;

public interface IGetOrdersByCustomerUseCase
{
    Task<OperationResult<IEnumerable<OrderResponseDto>>> ExecuteAsync(Guid customerId);
}
