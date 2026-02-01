using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetInvoiceById;

public class GetInvoiceByIdUseCase(
    IInvoiceRepository invoiceRepository
) : IGetInvoiceByIdUseCase
{
    public async Task<InvoiceResponseDto?> ExecuteAsync(Guid invoiceId)
    {
        var invoice = await invoiceRepository.GetByIdAsync(invoiceId);
        return invoice == null ? null : EntityToDtoMapping.MapInvoice(invoice);
    }
}
