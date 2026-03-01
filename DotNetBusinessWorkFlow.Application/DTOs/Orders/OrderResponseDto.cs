using DotNetBusinessWorkFlow.Application.Common;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Orders;

public sealed class OrderResponseDto : AuditableEntityDto
{
    public Guid CustomerId { get; init; }
    public OrderStatus Status { get; init; }
    public Money TotalAmount { get; init; } = Money.Zero("INR");
    public List<OrderItemResponseDto> Items { get; init; } = [];
}