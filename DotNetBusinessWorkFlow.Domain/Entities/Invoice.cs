using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Domain.Entities;

public class Invoice : AuditableEntity
{
    private readonly List<InvoiceItem> _items = new();

    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
    public string InvoiceNumber { get; private set; }
    public Money TotalAmount { get; private set; }
    public DateTime IssuedAt { get; private set; }

    public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();

    private Invoice() { }

    public Invoice(Guid orderId, Guid customerId)
    {
        OrderId = orderId;
        CustomerId = customerId;
        InvoiceNumber = GenerateInvoiceNumber();
        IssuedAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        TotalAmount = Money.Zero("INR");
    }

    public void AddItem(string productName, int quantity, Money unitPrice)
    {
        var item = new InvoiceItem(productName, quantity, unitPrice);
        _items.Add(item);
        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        TotalAmount = _items
            .Select(i => i.TotalPrice)
            .Aggregate(Money.Zero("INR"), (acc, next) => acc + next);
    }

    private static string GenerateInvoiceNumber()
    {
        return $"INV-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}".Substring(0, 20);
    }
}
