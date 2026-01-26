using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CancelOrder;

public class CancelOrderUseCase(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork
) : ICancelOrderUseCase
{
    public async Task<OrderResponseDto> ExecuteAsync(Guid orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId)
            ?? throw new Exception("Order not found");

        order.Cancel();

        await unitOfWork.SaveChangesAsync();

        return EntityToDtoMapping.MapOrder(order);
    }
}
