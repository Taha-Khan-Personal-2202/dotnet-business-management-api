using DotNetBusinessWorkFlow.Domain.ValueObjects;

namespace DotNetBusinessWorkFlow.Application.DTOs.Products;

public sealed record ProductRequestUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public Money Price { get; init; } = Money.Zero("INR");
}