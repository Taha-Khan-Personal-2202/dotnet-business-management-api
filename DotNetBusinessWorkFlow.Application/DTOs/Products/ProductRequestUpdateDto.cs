using DotNetBusinessWorkFlow.Application.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Products;

public class ProductRequestUpdateDto : AuditableEntityDto
{
    public string Name { get; private set; }
    public Money Price { get; private set; }
}
