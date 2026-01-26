using DotNetBusinessWorkFlow.Application.Common;
using DotNetBusinessWorkFlow.Application.DTOs.OrderItem;
using DotNetBusinessWorkFlow.Domain.Enums;

namespace DotNetBusinessWorkFlow.Application.DTOs.Orders;

public class OrderResponseDto : AuditableEntityDto
{
    public Guid CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemResponseDto> Items { get; set; } = [];
}
