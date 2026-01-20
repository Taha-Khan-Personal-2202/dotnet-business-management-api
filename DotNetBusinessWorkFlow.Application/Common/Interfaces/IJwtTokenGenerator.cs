using DotNetBusinessWorkflow.Domain.Entities;

namespace DotNetBusinessWorkFlow.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
