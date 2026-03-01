using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetInvoiceById;

public sealed class GetInvoiceByIdUseCase : IGetInvoiceByIdUseCase
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetInvoiceByIdUseCase(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<OperationResult<InvoiceResponseDto>> ExecuteAsync(Guid invoiceId)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
        if (invoice is null)
            return OperationResult<InvoiceResponseDto>.Error("Invoice not found.", 404);

        var dto = EntityToDtoMapping.MapInvoice(invoice);
        return OperationResult<InvoiceResponseDto>.Ok(dto, "Invoice retrieved successfully.");
    }
}