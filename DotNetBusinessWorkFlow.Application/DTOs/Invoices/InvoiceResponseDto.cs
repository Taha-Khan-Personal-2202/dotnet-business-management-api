using DotNetBusinessWorkFlow.Application.Common;

namespace DotNetBusinessWorkFlow.Application.DTOs.Invoices;

public class InvoiceResponseDto : AuditableEntityDto
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime IssuedAt { get; set; }
}
