using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CancelOrder;

public interface ICancelOrderUseCase
{
    Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId);
}
