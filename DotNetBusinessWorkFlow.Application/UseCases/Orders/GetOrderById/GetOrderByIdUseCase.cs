using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrderById;

public class GetOrderByIdUseCase(
    IOrderRepository orderRepository
) : IGetOrderByIdUseCase
{
    public async Task<OrderResponseDto?> ExecuteAsync(Guid orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        return EntityToDtoMapping.MapOrder(order);
    }
}
