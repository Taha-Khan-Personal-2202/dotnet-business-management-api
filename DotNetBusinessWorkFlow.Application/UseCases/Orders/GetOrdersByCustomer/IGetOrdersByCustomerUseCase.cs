using DotNetBusinessWorkFlow.Application.DTOs.Orders;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrdersByCustomer;

public interface IGetOrdersByCustomerUseCase
{
    Task<IEnumerable<OrderResponseDto>> ExecuteAsync(Guid customerId);
}
