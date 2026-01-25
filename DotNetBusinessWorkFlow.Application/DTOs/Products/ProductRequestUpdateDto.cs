using DotNetBusinessWorkFlow.Domain.Common;
using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Products;

public class ProductRequestUpdateDto : BaseEntity
{
    public string Name { get; private set; }
    public Money Price { get; private set; }
}
