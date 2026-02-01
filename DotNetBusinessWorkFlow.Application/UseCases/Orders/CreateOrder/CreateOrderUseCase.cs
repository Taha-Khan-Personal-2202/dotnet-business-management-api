using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CreateOrder;

public class CreateOrderUseCase(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : ICreateOrderUseCase
{
    public async Task<OrderResponseDto> ExecuteAsync(OrderRequestDto dto)
    {
        var customer = await customerRepository.GetByIdAsync(dto.CustomerId)
            ?? throw new Exception("Customer not found");

        if (!customer.IsActive)
            throw new Exception("Customer inactive");

        var order = new Order(dto.CustomerId);

        await orderRepository.AddAsync(order);
        await unitOfWork.SaveChangesAsync();

        return EntityToDtoMapping.MapOrder(order);
    }
}