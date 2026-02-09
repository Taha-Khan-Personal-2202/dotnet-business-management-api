using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Domain.Entities;

public class InvoiceItem : BaseEntity
{
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money TotalPrice { get; private set; }

    private InvoiceItem() { }

    public InvoiceItem(string productName, int quantity, Money unitPrice)
    {
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = unitPrice.Multiply(quantity);
    }
}
