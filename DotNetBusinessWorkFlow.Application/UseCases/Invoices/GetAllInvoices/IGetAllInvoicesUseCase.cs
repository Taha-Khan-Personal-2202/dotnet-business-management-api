using DotNetBusinessWorkFlow.Application.DTOs.Invoices;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetAllInvoices;

public interface IGetAllInvoicesUseCase
{
    Task<OperationResult<IEnumerable<InvoiceResponseDto>>> ExecuteAsync();
}
