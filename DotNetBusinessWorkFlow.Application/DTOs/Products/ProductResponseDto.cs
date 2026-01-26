using DotNetBusinessWorkFlow.Application.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Products;

public class ProductResponseDto : AuditableEntityDto
{
    public string Name { get; set; }
    public Money Price { get; set; }
    public bool IsActive { get; set; }
}
