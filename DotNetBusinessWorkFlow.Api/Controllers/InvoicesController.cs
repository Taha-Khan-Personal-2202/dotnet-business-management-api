using DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetAllInvoices;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetInvoiceById;
using DotNetBusinessWorkFlow.Application.UseCases.SendInvoiceEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{invoiceId}")]
    public async Task<IActionResult> GetById(Guid invoiceId)
    {
        var result = await _getById.ExecuteAsync(invoiceId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.ExecuteAsync();
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{invoiceId}/send-email")]
    public async Task<IActionResult> SendInvoiceEmail(Guid invoiceId)
    {
        var result = await _sendInvoiceEmailUseCase.ExecuteAsync(invoiceId);
        return StatusCode(result.StatusCode, result);
    }
}
