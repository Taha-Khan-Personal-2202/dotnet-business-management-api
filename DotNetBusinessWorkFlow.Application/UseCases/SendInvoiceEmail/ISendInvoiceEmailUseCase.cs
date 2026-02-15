using DotNetBusinessWorkFlow.Application.DTOs.Common;

namespace DotNetBusinessWorkFlow.Application.UseCases.SendInvoiceEmail;

public interface ISendInvoiceEmailUseCase
{
    Task<OperationResult<bool>> ExecuteAsync(Guid invoiceId);
}
