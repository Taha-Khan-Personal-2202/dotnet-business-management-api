using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetInvoiceById;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetAllInvoices;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/invoices")]
[Authorize(Roles = "Admin,Manager")]
public class InvoicesController : ControllerBase
{
    private readonly ICreateInvoiceUseCase _createInvoice;
    private readonly IGetInvoiceByIdUseCase _getById;
    private readonly IGetAllInvoicesUseCase _getAll;

    public InvoicesController(
        ICreateInvoiceUseCase createInvoice,
        IGetInvoiceByIdUseCase getById,
        IGetAllInvoicesUseCase getAll)
    {
        _createInvoice = createInvoice;
        _getById = getById;
        _getAll = getAll;
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
}
