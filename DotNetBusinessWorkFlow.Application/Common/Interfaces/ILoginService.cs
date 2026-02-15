using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using DotNetBusinessWorkFlow.Application.DTOs.Common;

namespace DotNetBusinessWorkFlow.Application.Common.Interfaces;

public interface ILoginService 
{
    Task<OperationResult<LoginResponseDto>> ExecuteAsync(LoginRequestDto dto);
}
