using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CompleteOrder;

public interface ICompleteOrderUseCase
{
    Task<OrderResponseDto> ExecuteAsync(Guid orderId);
}