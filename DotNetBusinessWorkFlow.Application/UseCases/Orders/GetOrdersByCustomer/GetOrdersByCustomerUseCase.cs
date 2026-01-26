using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrdersByCustomer;

public class GetOrdersByCustomerUseCase(
    IOrderRepository orderRepository
) : IGetOrdersByCustomerUseCase
{
    public async Task<IEnumerable<OrderResponseDto>> ExecuteAsync(Guid customerId)
    {
        var orders = await orderRepository.GetByCustomerIdAsync(customerId);
        return orders.Select(EntityToDtoMapping.MapOrder);
    }
}
