using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Domain.Entities;

public class Payment : AuditableEntity
{
    public Guid OrderId { get; private set; }
    public Money Amount { get; private set; }
    public PaymentStatus Status { get; private set; }

    private Payment() { }

    public Payment(Guid orderId, Money amount)
    {
        OrderId = orderId;
        Amount = amount;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsPaid()
    {
        if (Status == PaymentStatus.Paid)
            throw new InvalidOperationException("Payment already completed.");

        Status = PaymentStatus.Paid;
        MarkUpdated();
    }
}
