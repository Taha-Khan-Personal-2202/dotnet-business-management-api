// GetAllOrdersUseCase.cs
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetAllOrders;

public sealed class GetAllOrdersUseCase : IGetAllOrdersUseCase
{
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult<IEnumerable<OrderResponseDto>>> ExecuteAsync()
    {
        var orders = await _orderRepository.GetAllAsync();

        var dtos = orders.Select(EntityToDtoMapping.MapOrder).ToList();

        return OperationResult<IEnumerable<OrderResponseDto>>.Ok(dtos, $"Retrieved {dtos.Count} orders.");
    }
}