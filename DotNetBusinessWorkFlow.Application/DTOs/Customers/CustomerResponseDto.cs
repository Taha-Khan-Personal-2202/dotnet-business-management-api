using DotNetBusinessWorkFlow.Application.Common;

namespace DotNetBusinessWorkFlow.Application.DTOs.Customers;

public class CustomerResponseDto : AuditableEntityDto
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public bool IsActive { get; init; }
}