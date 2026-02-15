using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrderById;

public class GetOrderByIdUseCase(
    IOrderRepository orderRepository
) : IGetOrderByIdUseCase
{
    public async Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId)
    {
        var order = EntityToDtoMapping.MapOrder(await orderRepository.GetByIdAsync(orderId));
        return order is null
            ? OperationResult<OrderResponseDto>.Fail("Order not found.", 404)
            : OperationResult<OrderResponseDto>.Succces(order, "Order fetched successfully.");
    }
}
