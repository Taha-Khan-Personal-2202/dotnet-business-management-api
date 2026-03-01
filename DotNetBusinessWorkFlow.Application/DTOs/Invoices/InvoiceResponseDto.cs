using DotNetBusinessWorkFlow.Application.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Invoices;

public sealed class InvoiceResponseDto : AuditableEntityDto
{
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public string InvoiceNumber { get; init; } = string.Empty;
    public Money TotalAmount { get; init; } = Money.Zero("INR");
    public DateTime IssuedAt { get; init; }
}