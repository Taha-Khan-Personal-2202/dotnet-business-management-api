using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.AddOrderItem;

public class AddOrderItemUseCase(
    IOrderRepository orderRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : IAddOrderItemUseCase
{
    public async Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId, Guid productId, int quantity)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return OperationResult<OrderResponseDto>.Fail("Order not found.", 404);
        }

        var product = await productRepository.GetByIdAsync(productId);
        if (product is null)
        {
            return OperationResult<OrderResponseDto>.Fail("Product not found.", 404);
        }

        if (!product.IsActive)
        {
            return OperationResult<OrderResponseDto>.Fail("Product is inactive.", 400);
        }

        var item = new OrderItem(product.Id, quantity, product.Price);

        order.AddItem(item);

        await unitOfWork.SaveChangesAsync();

        return OperationResult<OrderResponseDto>.Succces(EntityToDtoMapping.MapOrder(order), "Order item added successfully.");
    }
}
