using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using DotNetBusinessWorkFlow.Application.DTOs.Common;

namespace DotNetBusinessWorkFlow.Application.Common.Interfaces;

public interface ILoginUseCase
{
    Task<OperationResult<LoginResponseDto>> ExecuteAsync(LoginRequestDto request);
}