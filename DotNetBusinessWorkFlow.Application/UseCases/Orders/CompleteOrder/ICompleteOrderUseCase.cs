using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CompleteOrder;

public interface ICompleteOrderUseCase
{
    Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId);
}
