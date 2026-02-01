using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetAllInvoices;

public class GetAllInvoicesUseCase(
    IInvoiceRepository invoiceRepository
) : IGetAllInvoicesUseCase
{
    public async Task<IEnumerable<InvoiceResponseDto>> ExecuteAsync()
    {
        var invoices = await invoiceRepository.GetAllAsync();
        return invoices.Select(EntityToDtoMapping.MapInvoice);
    }
}
