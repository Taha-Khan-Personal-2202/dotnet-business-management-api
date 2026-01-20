using DotNetBusinessWorkFlow.Application.DTOs.Auth;

namespace DotNetBusinessWorkFlow.Application.Common.Interfaces;

public interface ILoginService 
{
    Task<LoginResponseDto> ExecuteAsync(LoginRequestDto dto);
}
