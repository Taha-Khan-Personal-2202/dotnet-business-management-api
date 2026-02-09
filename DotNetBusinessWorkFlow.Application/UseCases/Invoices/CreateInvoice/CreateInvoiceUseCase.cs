using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;

public class CreateInvoiceUseCase(
    IOrderRepository orderRepository,
    IInvoiceRepository invoiceRepository,
    IUnitOfWork unitOfWork
) : ICreateInvoiceUseCase
{
    public async Task<InvoiceResponseDto> ExecuteAsync(Guid orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId)
            ?? throw new Exception("Order not found");

        if (order.Status != OrderStatus.Paid)
            throw new InvalidOperationException("Invoice can be created only for paid orders.");

        var existingInvoice = await invoiceRepository.GetByOrderIdAsync(orderId);
        if (existingInvoice != null)
            throw new InvalidOperationException("Invoice already exists for this order.");

        var invoice = new Invoice(
            order.Id,
            order.CustomerId
        );

        await invoiceRepository.AddAsync(invoice);
        await unitOfWork.SaveChangesAsync();

        return EntityToDtoMapping.MapInvoice(invoice);
    }
}
