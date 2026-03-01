using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;

public sealed class CreateInvoiceUseCase : ICreateInvoiceUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInvoiceUseCase(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IInvoiceRepository invoiceRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<InvoiceResponseDto>> ExecuteAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order is null)
            return OperationResult<InvoiceResponseDto>.Error("Order not found.", 404);

        if (order.Status != OrderStatus.Paid)
            return OperationResult<InvoiceResponseDto>.Error("Invoice can only be created for paid orders.", 400);

        var existing = await _invoiceRepository.GetByOrderIdAsync(orderId);
        if (existing is not null)
            return OperationResult<InvoiceResponseDto>.Error("Invoice already exists for this order.", 409);

        var invoice = new Invoice(order.Id, order.CustomerId);

        foreach (var item in order.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            invoice.AddItem(
                product?.Name ?? "Unknown Product",
                item.Quantity,
                item.UnitPrice
            );
        }

        await _invoiceRepository.AddAsync(invoice);
        await _unitOfWork.SaveChangesAsync();

        var dto = EntityToDtoMapping.MapInvoice(invoice);
        return OperationResult<InvoiceResponseDto>.Ok(dto, "Invoice created successfully.", 201);
    }
}