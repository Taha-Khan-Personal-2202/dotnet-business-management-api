using DotNetBusinessWorkflow.Domain.Repositories;
using DotNetBusinessWorkflow.Infrastructure.Authentication;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Infrastructure.Authentication;
using DotNetBusinessWorkFlow.Infrastructure.Data;
using DotNetBusinessWorkFlow.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBusinessWorkFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
