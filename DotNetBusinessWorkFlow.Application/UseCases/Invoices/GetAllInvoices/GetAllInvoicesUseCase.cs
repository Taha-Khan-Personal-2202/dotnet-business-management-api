using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetAllInvoices;

public sealed class GetAllInvoicesUseCase : IGetAllInvoicesUseCase
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetAllInvoicesUseCase(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<OperationResult<IEnumerable<InvoiceResponseDto>>> ExecuteAsync()
    {
        var invoices = await _invoiceRepository.GetAllAsync();
        var dtos = invoices.Select(EntityToDtoMapping.MapInvoice).ToList();

        return OperationResult<IEnumerable<InvoiceResponseDto>>.Ok(
            dtos,
            $"Retrieved {dtos.Count} invoices."
        );
    }
}