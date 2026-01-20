using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBusinessWorkFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ILoginService login) : ControllerBase
    {
        public readonly ILoginService _login = login;

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var result = await _login.ExecuteAsync(dto);
            return Ok(result);
        }
    }
}
