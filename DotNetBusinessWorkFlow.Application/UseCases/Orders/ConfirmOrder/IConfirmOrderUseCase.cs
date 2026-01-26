using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.ConfirmOrder;

public interface IConfirmOrderUseCase
{
    Task<OrderResponseDto> ExecuteAsync(Guid orderId);
}