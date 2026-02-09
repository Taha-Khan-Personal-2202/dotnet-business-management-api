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
    public async Task ExecuteAsync(Guid invoiceId)
    {
        var invoice = await invoiceRepository.GetByIdAsync(invoiceId)
            ?? throw new Exception("Invoice not found");

        var customer = await customerRepository.GetByIdAsync(invoice.CustomerId)
            ?? throw new Exception("Customer not found");


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

        await emailSender.SendAsync(
            customer.Email,
            $"Invoice {invoice.InvoiceNumber}",
            "Please find your invoice attached.",
            pdfBytes,
            $"Invoice-{invoice.InvoiceNumber}.pdf"
        );
    }
}
