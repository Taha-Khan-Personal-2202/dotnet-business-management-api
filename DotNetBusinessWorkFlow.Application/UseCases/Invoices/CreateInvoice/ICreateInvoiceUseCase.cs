using DotNetBusinessWorkFlow.Application.DTOs.Invoices;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;

public interface ICreateInvoiceUseCase
{
    Task<InvoiceResponseDto> ExecuteAsync(Guid orderId);
}
