using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkflow.Domain.Entities;

public class Invoice : AuditableEntity
{
    public Guid OrderId { get; private set; }
    public Money Amount { get; private set; }
    public InvoiceStatus Status { get; private set; }

    private Invoice() { }

    public Invoice(Guid orderId, Money amount)
    {
        OrderId = orderId;
        Amount = amount;
        Status = InvoiceStatus.Pending;
    }

    public void MarkAsPaid()
    {
        Status = InvoiceStatus.Paid;
        MarkUpdated();
    }
}
