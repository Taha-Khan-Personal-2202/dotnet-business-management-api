using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CreateOrder;

public class CreateOrderUseCase(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : ICreateOrderUseCase
{
    public async Task<OperationResult<OrderResponseDto>> ExecuteAsync(OrderRequestDto dto)
    {
        var customer = await customerRepository.GetByIdAsync(dto.CustomerId);
        if (customer is null)
        {
            return OperationResult<OrderResponseDto>.Fail("Customer not found.", 404);
        }

        if (!customer.IsActive)
        {
            return OperationResult<OrderResponseDto>.Fail("Customer is inactive.", 400);
        }

        var order = new Order(dto.CustomerId);

        await orderRepository.AddAsync(order);
        await unitOfWork.SaveChangesAsync();

        return OperationResult<OrderResponseDto>.Succces(EntityToDtoMapping.MapOrder(order), "Order created successfully.", 201);
    }
}
