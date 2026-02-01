using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Domain.Entities;

public class Invoice : AuditableEntity
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
    public string InvoiceNumber { get; private set; }
    public Money TotalAmount { get; private set; }
    public DateTime IssuedAt { get; private set; }

    private Invoice() { }

    public Invoice(Guid orderId, Guid customerId, Money totalAmount)
    {
        OrderId = orderId;
        CustomerId = customerId;
        TotalAmount = totalAmount;
        IssuedAt = DateTime.UtcNow;
        InvoiceNumber = GenerateInvoiceNumber();
        CreatedAt = DateTime.UtcNow;
    }

    private static string GenerateInvoiceNumber()
    {
        return $"INV-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6]}";
    }
}
