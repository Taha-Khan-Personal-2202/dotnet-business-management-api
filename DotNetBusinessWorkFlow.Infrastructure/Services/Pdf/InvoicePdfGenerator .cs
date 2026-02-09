using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.Common.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace DotNetBusinessWorkFlow.Infrastructure.Services.Pdf;

public class InvoicePdfGenerator : IInvoicePdfGenerator
{
    public byte[] Generate(InvoicePdfModel model)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);

                page.Content().Column(col =>
                {
                    col.Spacing(10);

                    col.Item().Text("INVOICE")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    col.Item().Text($"Invoice #: {model.InvoiceNumber}");
                    col.Item().Text($"Issued At: {model.IssuedAt:dd MMM yyyy}");
                    col.Item().Text($"Customer: {model.CustomerName}");
                    col.Item().Text($"Email: {model.CustomerEmail}");

                    col.Item().LineHorizontal(1);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Product").Bold();
                            header.Cell().Text("Qty").Bold();
                            header.Cell().Text("Unit Price").Bold();
                            header.Cell().Text("Total").Bold();
                        });

                        foreach (var item in model.Items)
                        {
                            table.Cell().Text(item.ProductName);
                            table.Cell().Text(item.Quantity.ToString());
                            table.Cell().Text(item.UnitPrice.ToString("0.00"));
                            table.Cell().Text(item.Total.ToString("0.00"));
                        }
                    });

                    col.Item().AlignRight().Text(
                        $"Total: {model.TotalAmount.Amount} {model.TotalAmount.Currency}"
                    ).Bold();
                });
            });
        }).GeneratePdf();
    }
}
