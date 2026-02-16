// ConfirmOrderUseCase.cs
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.ConfirmOrder;

public sealed class ConfirmOrderUseCase : IConfirmOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmOrderUseCase(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return OperationResult<OrderResponseDto>.Error("Order not found.", 404);
        }

        if (!order.CanConfirm(out var reason))
        {
            return OperationResult<OrderResponseDto>.Error(reason ?? "Cannot confirm this order.", 400);
        }

        order.Confirm();
        await _unitOfWork.SaveChangesAsync();

        var response = EntityToDtoMapping.MapOrder(order);
        return OperationResult<OrderResponseDto>.Ok(response, "Order confirmed successfully.");
    }
}