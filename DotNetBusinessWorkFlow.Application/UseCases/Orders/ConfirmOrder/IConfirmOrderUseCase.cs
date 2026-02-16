using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.ConfirmOrder;

public interface IConfirmOrderUseCase
{
    Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId);
}