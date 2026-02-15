using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.AddOrderItem;

public interface IAddOrderItemUseCase
{
    Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId, Guid productId, int quantity);
}
