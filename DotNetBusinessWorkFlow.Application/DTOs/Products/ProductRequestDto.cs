using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Products;

public class ProductRequestDto
{
    public string Name { get; set; } = default!;
    public Money Price { get; set; } = default!;
}
