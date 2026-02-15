using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.ConfirmOrder;

public class ConfirmOrderUseCase(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork
) : IConfirmOrderUseCase
{
    public async Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return OperationResult<OrderResponseDto>.Fail("Order not found.", 404);
        }

        order.Confirm();

        await unitOfWork.SaveChangesAsync();

        return OperationResult<OrderResponseDto>.Succces(EntityToDtoMapping.MapOrder(order), "Order confirmed successfully.");
    }
}
