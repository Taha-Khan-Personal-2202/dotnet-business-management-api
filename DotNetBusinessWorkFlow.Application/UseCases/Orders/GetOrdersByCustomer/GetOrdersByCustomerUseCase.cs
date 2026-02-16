using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrdersByCustomer;

public sealed class GetOrdersByCustomerUseCase : IGetOrdersByCustomerUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByCustomerUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult<IEnumerable<OrderResponseDto>>> ExecuteAsync(Guid customerId)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(customerId);

        var dtos = orders.Select(EntityToDtoMapping.MapOrder).ToList();

        return OperationResult<IEnumerable<OrderResponseDto>>.Ok(dtos, $"Retrieved {dtos.Count} orders for customer.");
    }
}