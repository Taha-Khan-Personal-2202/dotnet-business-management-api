using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.UseCases.Auth;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetAllCustomersUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByEmailUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByIdUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetAllInvoices;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetInvoiceById;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.SendInvoiceEmail;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.AddOrderItem;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.CancelOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.CompleteOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.ConfirmOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.CreateOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.GetAllOrders;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrderById;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrdersByCustomer;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.PayOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Payments.CreatePayment;
using DotNetBusinessWorkFlow.Application.UseCases.Payments.GetPaymentByOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Products.CreateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.GetAllProductsUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.GetProductByIdUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.SendInvoiceEmail;
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

        // Order Use Cases
        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IAddOrderItemUseCase, AddOrderItemUseCase>();
        services.AddScoped<IConfirmOrderUseCase, ConfirmOrderUseCase>();
        services.AddScoped<IPayOrderUseCase, PayOrderUseCase>();
        services.AddScoped<ICompleteOrderUseCase, CompleteOrderUseCase>();
        services.AddScoped<ICancelOrderUseCase, CancelOrderUseCase>();
        services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
        services.AddScoped<IGetAllOrdersUseCase, GetAllOrdersUseCase>();
        services.AddScoped<IGetOrdersByCustomerUseCase, GetOrdersByCustomerUseCase>();

        // Payment Use Cases
        services.AddScoped<ICreatePaymentUseCase, CreatePaymentUseCase>();
        services.AddScoped<IGetPaymentUseCase, GetPaymentUseCase>();

        // Invoice Use Cases
        services.AddScoped<ICreateInvoiceUseCase, CreateInvoiceUseCase>();
        services.AddScoped<IGetAllInvoicesUseCase, GetAllInvoicesUseCase>();
        services.AddScoped<IGetInvoiceByIdUseCase, GetInvoiceByIdUseCase>();

        // Invoice Email
        services.AddScoped<ISendInvoiceEmailUseCase, SendInvoiceEmailUseCase>();

        return services;
    }
}
