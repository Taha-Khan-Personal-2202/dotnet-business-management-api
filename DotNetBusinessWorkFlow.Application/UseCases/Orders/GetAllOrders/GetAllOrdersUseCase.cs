using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetAllOrders;

public class GetAllOrdersUseCase(
    IOrderRepository orderRepository
) : IGetAllOrdersUseCase
{
    public async Task<IEnumerable<OrderResponseDto>> ExecuteAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return orders.Select(EntityToDtoMapping.MapOrder);
    }
}
