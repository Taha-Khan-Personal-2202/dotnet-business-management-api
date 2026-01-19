using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace DotNetBusinessWorkFlow.Infrastructure.Authentication;

public class JwtTokenGenerator(IConfiguration config) : IJwtTokenGenerator
{
    public readonly IConfiguration _config = config;

    public string GenerateToken(User user)
    {
        return string.Empty;
    }
}
