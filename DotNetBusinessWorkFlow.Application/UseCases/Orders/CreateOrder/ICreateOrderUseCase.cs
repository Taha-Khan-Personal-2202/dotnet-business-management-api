using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CreateOrder;

public interface ICreateOrderUseCase
{
    Task<OrderResponseDto> ExecuteAsync(OrderRequestDto dto);
}
