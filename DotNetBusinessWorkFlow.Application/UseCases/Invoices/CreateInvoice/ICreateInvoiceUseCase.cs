using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Invoices;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;

public interface ICreateInvoiceUseCase
{
    Task<OperationResult<InvoiceResponseDto>> ExecuteAsync(Guid orderId);
}
