using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.Validators.Auth;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.UseCases.Auth;

public sealed class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IValidator<LoginRequestDto> _validator;

    public LoginUseCase(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator,
        IValidator<LoginRequestDto> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
        _validator = validator;
    }

    public async Task<OperationResult<LoginResponseDto>> ExecuteAsync(LoginRequestDto request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<LoginResponseDto>.Error(errors, 400);
        }

        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            return OperationResult<LoginResponseDto>.Error("Invalid email or password.", 401);
        }

        if (!user.IsActive)
        {
            return OperationResult<LoginResponseDto>.Error("Account is disabled.", 403);
        }

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            return OperationResult<LoginResponseDto>.Error("Invalid email or password.", 401);
        }

        var token = _tokenGenerator.GenerateToken(user);

        var response = new LoginResponseDto
        {
            Token = token,
            Role = user.Role.ToString()
        };

        return OperationResult<LoginResponseDto>.Ok(response);
    }
}