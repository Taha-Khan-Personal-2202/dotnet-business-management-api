namespace DotNetBusinessWorkFlow.Application.UseCases.SendInvoiceEmail;

public interface ISendInvoiceEmailUseCase
{
    Task ExecuteAsync(Guid invoiceId);
}
