using DotNetBusinessWorkFlow.Domain.Common;

namespace DotNetBusinessWorkFlow.Application.DTOs.Customers;

public class CustomerRequestDto
{
    public string Name { get; private set; }
    public string Email { get; private set; }
}
