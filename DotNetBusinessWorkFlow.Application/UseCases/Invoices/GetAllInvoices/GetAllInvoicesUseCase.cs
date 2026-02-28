using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetAllInvoices;

public class GetAllInvoicesUseCase(
    IInvoiceRepository invoiceRepository
) : IGetAllInvoicesUseCase
{
    public async Task<OperationResult<IEnumerable<InvoiceResponseDto>>> ExecuteAsync()
    {
        var invoices = await invoiceRepository.GetAllAsync();
        return OperationResult<IEnumerable<InvoiceResponseDto>>.Ok(invoices.Select(EntityToDtoMapping.MapInvoice));
    }
}
