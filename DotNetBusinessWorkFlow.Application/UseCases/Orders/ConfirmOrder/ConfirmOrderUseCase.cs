using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.ConfirmOrder;

public class ConfirmOrderUseCase(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork
) : IConfirmOrderUseCase
{
    public async Task<OrderResponseDto> ExecuteAsync(Guid orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId)
            ?? throw new Exception("Order not found");

        order.Confirm();

        await unitOfWork.SaveChangesAsync();

        return EntityToDtoMapping.MapOrder(order);
    }
}
