using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetInvoiceById;

public class GetInvoiceByIdUseCase(
    IInvoiceRepository invoiceRepository
) : IGetInvoiceByIdUseCase
{
    public async Task<OperationResult<InvoiceResponseDto>> ExecuteAsync(Guid invoiceId)
    {
        var invoice = await invoiceRepository.GetByIdAsync(invoiceId);
        if (invoice == null) return OperationResult<InvoiceResponseDto>.Error("Invoice not found.");

        return OperationResult<InvoiceResponseDto>.Ok(EntityToDtoMapping.MapInvoice(invoice));
    }
}
