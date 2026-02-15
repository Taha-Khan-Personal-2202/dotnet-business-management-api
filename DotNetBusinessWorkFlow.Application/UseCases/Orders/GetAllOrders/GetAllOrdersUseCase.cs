using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetAllOrders;

public class GetAllOrdersUseCase(
    IOrderRepository orderRepository
) : IGetAllOrdersUseCase
{
    public async Task<OperationResult<IEnumerable<OrderResponseDto>>> ExecuteAsync()
    {
        var orders = (await orderRepository.GetAllAsync()).Select(EntityToDtoMapping.MapOrder).ToList();
        return OperationResult<IEnumerable<OrderResponseDto>>.Succces(orders, "Orders fetched successfully.");
    }
}
