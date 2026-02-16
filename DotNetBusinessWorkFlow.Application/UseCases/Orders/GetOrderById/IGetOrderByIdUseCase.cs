using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrderById;

public interface IGetOrderByIdUseCase
{
    Task<OperationResult<OrderResponseDto?>> ExecuteAsync(Guid orderId);
}
