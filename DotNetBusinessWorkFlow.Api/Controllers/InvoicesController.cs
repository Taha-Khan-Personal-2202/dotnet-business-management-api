using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetInvoiceById;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetAllInvoices;
using DotNetBusinessWorkFlow.Application.UseCases.SendInvoiceEmail;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/invoices")]
[Authorize(Roles = "Admin,Manager")]
public class InvoicesController : ControllerBase
{
    private readonly ICreateInvoiceUseCase _createInvoice;
    private readonly IGetInvoiceByIdUseCase _getById;
    private readonly IGetAllInvoicesUseCase _getAll;
    private readonly ISendInvoiceEmailUseCase _sendInvoiceEmailUseCase;

    public InvoicesController(
        ICreateInvoiceUseCase createInvoice,
        IGetInvoiceByIdUseCase getById,
        IGetAllInvoicesUseCase getAll,
        ISendInvoiceEmailUseCase sendInvoiceEmailUseCase)
    {
        _createInvoice = createInvoice;
        _getById = getById;
        _getAll = getAll;
        _sendInvoiceEmailUseCase = sendInvoiceEmailUseCase;
    }

    [HttpPost("{orderId}")]
    public async Task<IActionResult> Create(Guid orderId)
    {
        var result = await _createInvoice.ExecuteAsync(orderId);
        return Ok(result);
    }

    [HttpGet("{invoiceId}")]
    public async Task<IActionResult> GetById(Guid invoiceId)
    {
        var result = await _getById.ExecuteAsync(invoiceId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.ExecuteAsync();
        return Ok(result);
    }

    [HttpPost("{invoiceId}/send-email")]
    public async Task<IActionResult> SendInvoiceEmail(Guid invoiceId)
    {
        await _sendInvoiceEmailUseCase.ExecuteAsync(invoiceId);
        return Ok(new { message = "Invoice email sent successfully." });
    }
}
