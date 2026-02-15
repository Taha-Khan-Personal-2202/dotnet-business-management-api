using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.PayOrder;

public interface IPayOrderUseCase
{
    Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId);
}
