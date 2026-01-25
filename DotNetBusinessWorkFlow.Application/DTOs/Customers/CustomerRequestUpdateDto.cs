using DotNetBusinessWorkFlow.Domain.Common;

namespace DotNetBusinessWorkFlow.Application.DTOs.Customers;

public class CustomerRequestUpdateDto : AuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}
