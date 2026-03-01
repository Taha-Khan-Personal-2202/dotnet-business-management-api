namespace DotNetBusinessWorkFlow.Application.DTOs.Orders;

public sealed record OrderRequestDto
{
    public Guid CustomerId { get; set; }
}
