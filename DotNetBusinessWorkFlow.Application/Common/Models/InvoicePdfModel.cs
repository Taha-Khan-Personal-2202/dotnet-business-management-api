using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.Common.Models;

public class InvoicePdfModel
{
    public string InvoiceNumber { get; set; } = default!;
    public DateTime IssuedAt { get; set; }
    public string CustomerName { get; set; } = default!;
    public string CustomerEmail { get; set; } = default!;
    public Money TotalAmount { get; set; } = default!;
    public List<InvoiceItemPdfModel> Items { get; set; } = new();
}

public class InvoiceItemPdfModel
{
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => UnitPrice * Quantity;
}
