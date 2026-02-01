using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Payments;

public class PaymentRequestDto
{
    public Guid OrderId { get; set; }
    public Money Amount { get; set; }
}
