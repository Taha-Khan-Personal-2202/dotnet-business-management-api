using DotNetBusinessWorkFlow.Application.DTOs.Common;

namespace DotNetBusinessWorkFlow.Application.DTOs.Customers;

public class CustomerRequestUpdateDto : BaseEntityDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}
