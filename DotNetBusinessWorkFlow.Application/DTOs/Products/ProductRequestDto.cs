using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Products;

public class ProductRequestDto
{
    public string Name { get; private set; }
    public Money Price { get; private set; }
}
