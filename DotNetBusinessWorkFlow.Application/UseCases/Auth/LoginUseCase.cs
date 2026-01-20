using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Auth;

public class LoginUseCase(
    IUserRepository repository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator generator
) : ILoginService
{
    private readonly IUserRepository _repository = repository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IJwtTokenGenerator _generator = generator;

    public async Task<LoginResponseDto> ExecuteAsync(LoginRequestDto dto)
    {
        var user = await _repository.GetByEmailAsync(dto.Email)
            ?? throw new Exception("User not found");

        if (!user.IsActive)
            throw new Exception("Unauthorized access");

        if (!_passwordHasher.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return new LoginResponseDto
        {
            Token = _generator.GenerateToken(user),
            Role = user.Role.ToString()
        };
    }
}
