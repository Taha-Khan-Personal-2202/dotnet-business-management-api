using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.UseCases.Auth;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetAllCustomersUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByEmailUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByIdUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.CreateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.GetAllProductsUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.GetProductByIdUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBusinessWorkFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginUseCase>();

        // Product Use Cases
        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
        services.AddScoped<IDeactivateProductUseCase, DeactivateProductUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();

        // Customer Use Cases
        services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
        services.AddScoped<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
        services.AddScoped<IGetCustomerByEmailUseCase, GetCustomerByEmailUseCase>();
        services.AddScoped<IGetAllCustomersUseCase, GetAllCustomersUseCase>();
        services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();

        return services;
    }
}
