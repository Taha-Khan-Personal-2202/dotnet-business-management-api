using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CreateOrder;

public interface ICreateOrderUseCase
{
    Task<OperationResult<OrderResponseDto>> ExecuteAsync(OrderRequestDto dto);
}
