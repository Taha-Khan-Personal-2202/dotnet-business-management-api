using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkflow.Domain.Entities;

public class Payment : AuditableEntity
{
    public Guid InvoiceId { get; private set; }
    public Money Amount { get; private set; }
    public PaymentStatus Status { get; private set; }

    private Payment() { }

    public Payment(Guid invoiceId, Money amount)
    {
        InvoiceId = invoiceId;
        Amount = amount;
        Status = PaymentStatus.Recorded;
    }
}
