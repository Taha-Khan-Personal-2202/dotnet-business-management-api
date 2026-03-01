using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBusinessWorkFlow.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;

    public AuthController(ILoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(OperationResult<LoginResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<LoginResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<LoginResponseDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(OperationResult<LoginResponseDto>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _loginUseCase.ExecuteAsync(request);
        return StatusCode(result.StatusCode, result);
    }
}