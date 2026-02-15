using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CancelOrder;

public class CancelOrderUseCase(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork
) : ICancelOrderUseCase
{
    public async Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return OperationResult<OrderResponseDto>.Fail("Order not found.", 404);
        }

        order.Cancel();

        await unitOfWork.SaveChangesAsync();

        return OperationResult<OrderResponseDto>.Succces(EntityToDtoMapping.MapOrder(order), "Order cancelled successfully.");
    }
}
