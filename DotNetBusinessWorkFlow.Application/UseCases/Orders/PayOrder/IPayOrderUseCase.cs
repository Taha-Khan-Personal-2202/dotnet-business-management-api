using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.PayOrder;

public interface IPayOrderUseCase
{
    Task<OrderResponseDto> ExecuteAsync(Guid orderId);
}