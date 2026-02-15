using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetAllOrders;

public interface IGetAllOrdersUseCase
{
    Task<OperationResult<IEnumerable<OrderResponseDto>>> ExecuteAsync();
}
