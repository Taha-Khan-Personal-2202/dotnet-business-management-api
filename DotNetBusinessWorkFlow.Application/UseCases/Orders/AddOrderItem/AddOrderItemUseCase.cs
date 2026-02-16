using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.AddOrderItem;

public sealed class AddOrderItemUseCase : IAddOrderItemUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderItemUseCase(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<OrderResponseDto>> ExecuteAsync(Guid orderId, Guid productId, int quantity)
    {
        if (quantity <= 0)
        {
            return OperationResult<OrderResponseDto>.Error("Quantity must be greater than zero.", 400);
        }

        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return OperationResult<OrderResponseDto>.Error("Order not found.", 404);
        }

        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
        {
            return OperationResult<OrderResponseDto>.Error("Product not found.", 404);
        }

        if (!product.IsActive)
        {
            return OperationResult<OrderResponseDto>.Error("Product is inactive.", 400);
        }

        var item = new OrderItem(product.Id, quantity, product.Price);
        order.AddItem(item);

        await _unitOfWork.SaveChangesAsync();

        var response = EntityToDtoMapping.MapOrder(order);
        return OperationResult<OrderResponseDto>.Ok(response, "Item added to order.");
    }
}