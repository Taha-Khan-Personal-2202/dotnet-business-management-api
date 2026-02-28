using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.Common.Models;
using DotNetBusinessWorkFlow.Application.UseCases.SendInvoiceEmail;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.SendInvoiceEmail;

public class SendInvoiceEmailUseCase(
    IInvoiceRepository invoiceRepository,
    ICustomerRepository customerRepository,
    IInvoicePdfGenerator pdfGenerator,
    IEmailSender emailSender
) : ISendInvoiceEmailUseCase
{
    public async Task<OperationResult<string>> ExecuteAsync(Guid invoiceId)
    {
        var invoice = await invoiceRepository.GetByIdAsync(invoiceId);
        if (invoice == null)
            return OperationResult<string>.Error("Invoice not found.");

        var customer = await customerRepository.GetByIdAsync(invoice.CustomerId);
        if (customer == null)
            return OperationResult<string>.Error("Customer not found.");

        var pdfModel = new InvoicePdfModel
        {
            InvoiceNumber = invoice.InvoiceNumber,
            IssuedAt = invoice.IssuedAt,
            CustomerName = customer.Name,
            CustomerEmail = customer.Email,
            TotalAmount = invoice.TotalAmount,
            Items = invoice.Items.Select(i => new InvoiceItemPdfModel
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice.Amount
            }).ToList()
        };

        var pdfBytes = pdfGenerator.Generate(pdfModel);

        try
        {
            await emailSender.SendAsync(
                customer.Email,
                $"Invoice {invoice.InvoiceNumber}",
                "Please find your invoice attached.",
                pdfBytes,
                $"Invoice-{invoice.InvoiceNumber}.pdf"
            );
        }
        catch (Exception e)
        {
            return OperationResult<string>.Error(e.Message);
        }

        return OperationResult<string>.Ok("Invoice email sent successfully.");
    }
}
