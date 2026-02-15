using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
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
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return OperationResult<InvoiceResponseDto>.Fail("Order not found.", 404);
        }

        if (order.Status != OrderStatus.Paid)
        {
            return OperationResult<InvoiceResponseDto>.Fail("Invoice can be created only for paid orders.", 400);
        }

        var existingInvoice = await invoiceRepository.GetByOrderIdAsync(orderId);
        if (existingInvoice != null)
        {
            return OperationResult<InvoiceResponseDto>.Fail("Invoice already exists for this order.", 409);
        }

        var invoice = new Invoice(order.Id, order.CustomerId);

        foreach (var item in order.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            invoice.AddItem(product?.Name ?? string.Empty, item.Quantity, item.UnitPrice);
        }

        await invoiceRepository.AddAsync(invoice);
        await unitOfWork.SaveChangesAsync();

        return OperationResult<InvoiceResponseDto>.Succces(EntityToDtoMapping.MapInvoice(invoice), "Invoice created successfully.", 201);
    }
}
