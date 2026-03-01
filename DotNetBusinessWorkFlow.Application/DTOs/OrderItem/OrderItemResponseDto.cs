using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Orders;

public sealed record OrderItemResponseDto
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public Money UnitPrice { get; init; } = Money.Zero("INR");
    public Money TotalPrice { get; init; } = Money.Zero("INR");
}