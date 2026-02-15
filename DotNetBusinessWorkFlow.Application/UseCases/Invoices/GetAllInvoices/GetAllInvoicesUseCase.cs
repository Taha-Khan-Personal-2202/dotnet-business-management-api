using DotNetBusinessWorkFlow.Application.DTOs.Common;
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
        var invoices = (await invoiceRepository.GetAllAsync()).Select(EntityToDtoMapping.MapInvoice).ToList();
        return OperationResult<IEnumerable<InvoiceResponseDto>>.Succces(invoices, "Invoices fetched successfully.");
    }
}
