using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkflow.Domain.Repositories;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.AddOrderItem;

public class AddOrderItemUseCase(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : IAddOrderItemUseCase
{
    public async Task<OrderResponseDto> ExecuteAsync(Guid orderId, Guid productId, int quantity)
    {
        var order = await orderRepository.GetByIdAsync(orderId)
            ?? throw new Exception("Order not found");

        var product = await productRepository.GetByIdAsync(productId)
            ?? throw new Exception("Product not found");

        if (!product.IsActive)
            throw new Exception("Product inactive");

        var item = new OrderItem(product.Id, quantity, product.Price);

        order.AddItem(item);

        await unitOfWork.SaveChangesAsync();

        return EntityToDtoMapping.MapOrder(order);
    }
}
