namespace DotNetBusinessWorkFlow.Application.UseCases.SendInvoiceEmail;

public interface ISendInvoiceEmailUseCase
{
    Task<OperationResult<string>> ExecuteAsync(Guid invoiceId);
}
