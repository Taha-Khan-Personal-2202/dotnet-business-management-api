using DotNetBusinessWorkFlow.Application.Common.Models;

namespace DotNetBusinessWorkFlow.Application.Common.Interfaces;

public interface IInvoicePdfGenerator
{
    byte[] Generate(InvoicePdfModel model);
}