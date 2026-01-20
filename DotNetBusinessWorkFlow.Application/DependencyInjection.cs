using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.UseCases.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBusinessWorkFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginUseCase>();
        return services;
    }
}
