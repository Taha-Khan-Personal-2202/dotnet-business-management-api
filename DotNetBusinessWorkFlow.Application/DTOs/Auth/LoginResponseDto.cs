namespace DotNetBusinessWorkFlow.Application.DTOs.Auth;

public sealed record LoginResponseDto
{
    public string Token { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}