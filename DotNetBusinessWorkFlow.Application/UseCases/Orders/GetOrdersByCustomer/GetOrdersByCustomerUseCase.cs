using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrdersByCustomer;

public class GetOrdersByCustomerUseCase(
    IOrderRepository orderRepository
) : IGetOrdersByCustomerUseCase
{
    public async Task<OperationResult<IEnumerable<OrderResponseDto>>> ExecuteAsync(Guid customerId)
    {
        var orders = (await orderRepository.GetByCustomerIdAsync(customerId)).Select(EntityToDtoMapping.MapOrder).ToList();
        return OperationResult<IEnumerable<OrderResponseDto>>.Succces(orders, "Customer orders fetched successfully.");
    }
}
