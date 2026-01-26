using DotNetBusinessWorkFlow.Application.Common;

namespace DotNetBusinessWorkFlow.Application.DTOs.Customers;

public class CustomerResponseDto : AuditableEntityDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}
