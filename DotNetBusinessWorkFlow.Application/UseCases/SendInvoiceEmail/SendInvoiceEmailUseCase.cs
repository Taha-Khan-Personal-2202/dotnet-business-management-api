using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.Common.Models;
using DotNetBusinessWorkFlow.Application.UseCases.SendInvoiceEmail;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Domain.Repositories;

namespace DotNetBusinessWorkFlow.Application.UseCases.Invoices.SendInvoiceEmail;

public sealed class SendInvoiceEmailUseCase : ISendInvoiceEmailUseCase
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IInvoicePdfGenerator _pdfGenerator;
    private readonly IEmailSender _emailSender;

    public SendInvoiceEmailUseCase(
        IInvoiceRepository invoiceRepository,
        ICustomerRepository customerRepository,
        IInvoicePdfGenerator pdfGenerator,
        IEmailSender emailSender)
    {
        _invoiceRepository = invoiceRepository;
        _customerRepository = customerRepository;
        _pdfGenerator = pdfGenerator;
        _emailSender = emailSender;
    }

    public async Task<OperationResult<string>> ExecuteAsync(Guid invoiceId)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
        if (invoice is null)
            return OperationResult<string>.Error("Invoice not found.", 404);

        var customer = await _customerRepository.GetByIdAsync(invoice.CustomerId);
        if (customer is null)
            return OperationResult<string>.Error("Customer not found.", 404);

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

        var pdfBytes = _pdfGenerator.Generate(pdfModel);

        try
        {
            await _emailSender.SendAsync(
                to: customer.Email,
                subject: $"Your Invoice {invoice.InvoiceNumber}",
                body: "Please find your invoice attached as PDF.",
                pdfBytes,
                $"Invoice-{invoice.InvoiceNumber}.pdf"
            );

            return OperationResult<string>.Ok("Invoice email sent successfully.");
        }
        catch (Exception)
        {
            return OperationResult<string>.Error("Failed to send invoice email. Please try again later.", 500);
        }
    }
}