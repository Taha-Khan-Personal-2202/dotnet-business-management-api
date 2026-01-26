using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.AddOrderItem;

public interface IAddOrderItemUseCase
{
    Task<OrderResponseDto> ExecuteAsync(Guid orderId, Guid productId, int quantity);
}