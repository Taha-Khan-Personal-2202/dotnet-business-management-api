using DotNetBusinessWorkFlow.Application.Common;
using DotNetBusinessWorkFlow.Domain.Enums;

namespace DotNetBusinessWorkFlow.Application.DTOs.Payments;

public class PaymentResponseDto : AuditableEntityDto
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
}
