using DotNetBusinessWorkFlow.Application.Common;

namespace DotNetBusinessWorkFlow.Application.DTOs.Customers;

public class CustomerResponseDto : AuditableEntityDto
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
}
