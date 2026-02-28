using DotNetBusinessWorkFlow.Application.DTOs.Invoices;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetInvoiceById;

public interface IGetInvoiceByIdUseCase
{
    Task<OperationResult<InvoiceResponseDto>> ExecuteAsync(Guid invoiceId);
}
