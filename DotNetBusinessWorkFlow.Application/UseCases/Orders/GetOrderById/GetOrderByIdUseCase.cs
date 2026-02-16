using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrderById;

public sealed class GetOrderByIdUseCase : IGetOrderByIdUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult<OrderResponseDto?>> ExecuteAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return OperationResult<OrderResponseDto?>.Error("Order not found.", 404, null);
        }

        var response = EntityToDtoMapping.MapOrder(order);
        return OperationResult<OrderResponseDto?>.Ok(response, "Order retrieved successfully.");
    }
}