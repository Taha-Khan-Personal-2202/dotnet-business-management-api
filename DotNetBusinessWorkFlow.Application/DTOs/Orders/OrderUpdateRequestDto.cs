namespace DotNetBusinessWorkFlow.Application.DTOs.Orders;

public class OrderUpdateRequestDto
{
    public Guid OrderId { get; set; }

    public Guid? ProductId { get; set; }
    public int? Quantity { get; set; }
}
