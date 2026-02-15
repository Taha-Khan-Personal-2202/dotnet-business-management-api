using DotNetBusinessWorkFlow.Application.DTOs.Common;
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
        return invoice is null
            ? OperationResult<InvoiceResponseDto>.Fail("Invoice not found.", 404)
            : OperationResult<InvoiceResponseDto>.Succces(EntityToDtoMapping.MapInvoice(invoice), "Invoice fetched successfully.");
    }
}
