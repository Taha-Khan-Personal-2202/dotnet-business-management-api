using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.Validators.Auth;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Auth;

public class LoginUseCase(
    IUserRepository repository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator generator,
    LoginValidator validations
) : ILoginService
{
    private readonly IUserRepository _repository = repository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IJwtTokenGenerator _generator = generator;
    private readonly LoginValidator _validations = validations;

    public async Task<OperationResult<LoginResponseDto>> ExecuteAsync(LoginRequestDto dto)
    {
        var validaitonResult = await _validations.ValidateAsync(dto);
        if (!validaitonResult.IsValid)
        {
            var errors = string.Join(";", validaitonResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<LoginResponseDto>.Fail($"Validation failed: {errors}", 400);
        }

        var user = await _repository.GetByEmailAsync(dto.Email);
        if (user == null) return OperationResult<LoginResponseDto>.Fail($"User not found.", 404);

        if (!user.IsActive)
            return OperationResult<LoginResponseDto>.Fail($"Unauthorized access.", 401);

        if (!_passwordHasher.Verify(dto.Password, user.PasswordHash))
            return OperationResult<LoginResponseDto>.Fail($"Invalid credintials.", 400);

        var response = new LoginResponseDto
        {
            Token = _generator.GenerateToken(user),
            Role = user.Role.ToString()
        };

        return OperationResult<LoginResponseDto>.Succces(response);
    }
}
