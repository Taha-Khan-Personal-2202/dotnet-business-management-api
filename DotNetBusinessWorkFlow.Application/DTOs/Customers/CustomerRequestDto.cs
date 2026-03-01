namespace DotNetBusinessWorkFlow.Application.DTOs.Customers;

public sealed record CustomerRequestDto
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}