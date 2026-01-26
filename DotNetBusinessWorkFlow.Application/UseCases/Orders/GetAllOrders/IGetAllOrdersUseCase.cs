using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetAllOrders;

public interface IGetAllOrdersUseCase
{
    Task<IEnumerable<OrderResponseDto>> ExecuteAsync();
}
