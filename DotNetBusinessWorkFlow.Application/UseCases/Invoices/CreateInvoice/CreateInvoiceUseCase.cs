using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;

public class CreateInvoiceUseCase(
    IProductRepository productRepository,
    IOrderRepository orderRepository,
    IInvoiceRepository invoiceRepository,
    IUnitOfWork unitOfWork
) : ICreateInvoiceUseCase
{
    public async Task<OperationResult<InvoiceResponseDto>> ExecuteAsync(Guid orderId)
    {
        // getting order
        var order = await orderRepository.GetByIdAsync(orderId);

        if (order == null)
            return OperationResult<InvoiceResponseDto>.Error("Order not found.");

        // checking status
        if (order.Status == OrderStatus.Created || order.Status == OrderStatus.Confirmed)
            return OperationResult<InvoiceResponseDto>.Error("Invoice can be created only for paid orders.");

        // getting inovice
        var existingInvoice = await invoiceRepository.GetByOrderIdAsync(orderId);
        if (existingInvoice != null)
            return OperationResult<InvoiceResponseDto>.Error("Invoice already exists for this order.");

        var invoice = new Invoice(
            order.Id,
            order.CustomerId
        );

        foreach (var item in order.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            invoice.AddItem(product?.Name ?? string.Empty,
                item.Quantity,
                item.UnitPrice);
        }

        await invoiceRepository.AddAsync(invoice);
        await unitOfWork.SaveChangesAsync();

        return OperationResult<InvoiceResponseDto>.Ok(EntityToDtoMapping.MapInvoice(invoice));
    }
}
