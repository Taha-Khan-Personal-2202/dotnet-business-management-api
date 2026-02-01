using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.DTOs.OrderItem;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.DTOs.Products;

namespace DotNetBusinessWorkFlow.Application.Mappings;

public static class EntityToDtoMapping
{
    public static ProductResponseDto? MapProduct(Product? product)
    {
        if (product == null) return null;
        return new ProductResponseDto()
        {
            CreatedAt = product.CreatedAt,
            Id = product.Id,
            IsActive = product.IsActive,
            Name = product.Name,
            Price = product.Price,
            UpdateAt = product.UpdateAt,
        };
    }

    public static CustomerResponseDto? MapCustomer(Customer? customer)
    {
        if (customer == null) return null;
        return new CustomerResponseDto()
        {
            Email = customer.Email,
            Name = customer.Name,
            CreatedAt = customer.CreatedAt,
            Id = customer.Id,
            IsActive = customer.IsActive,
            UpdateAt = customer.UpdateAt
        };
    }

    public static OrderResponseDto MapOrder(Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Status = order.Status,
            TotalAmount = order.TotalAmount.Amount,
            Items = order.Items.Select(MapOrderItemEntityToDto).ToList(),
            CreatedAt = order.CreatedAt,
            UpdateAt = order.UpdateAt
        };
    }

    private static OrderItemResponseDto MapOrderItemEntityToDto(OrderItem item)
    {
        return new OrderItemResponseDto
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice.Amount,
            TotalPrice = item.GetTotal().Amount
        };
    }
}