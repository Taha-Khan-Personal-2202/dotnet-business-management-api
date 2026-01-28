using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Products;

public class ProductRequestUpdateDto : BaseEntityDto
{
    public string Name { get; set; } = default!;
    public Money Price { get; set; } = default!;
}
