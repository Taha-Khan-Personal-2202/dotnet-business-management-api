using DotNetBusinessWorkFlow.Application.DTOs.Invoices;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.CreateInvoice;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetAllInvoices;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.GetInvoiceById;
using DotNetBusinessWorkFlow.Application.UseCases.Invoices.SendInvoiceEmail;
using DotNetBusinessWorkFlow.Application.UseCases.SendInvoiceEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/invoices")]
[Authorize(Roles = "Admin,Manager")]
public class InvoicesController : ControllerBase
{
    private readonly ICreateInvoiceUseCase _createInvoice;
    private readonly IGetInvoiceByIdUseCase _getById;
    private readonly IGetAllInvoicesUseCase _getAll;
    private readonly ISendInvoiceEmailUseCase _sendInvoiceEmail;

    public InvoicesController(
        ICreateInvoiceUseCase createInvoice,
        IGetInvoiceByIdUseCase getById,
        IGetAllInvoicesUseCase getAll,
        ISendInvoiceEmailUseCase sendInvoiceEmail)
    {
        _createInvoice = createInvoice;
        _getById = getById;
        _getAll = getAll;
        _sendInvoiceEmail = sendInvoiceEmail;
    }

    [HttpPost("{orderId}")]
    [ProducesResponseType(typeof(OperationResult<InvoiceResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(OperationResult<InvoiceResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<InvoiceResponseDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(OperationResult<InvoiceResponseDto>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(Guid orderId)
    {
        var result = await _createInvoice.ExecuteAsync(orderId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{invoiceId}")]
    [ProducesResponseType(typeof(OperationResult<InvoiceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<InvoiceResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid invoiceId)
    {
        var result = await _getById.ExecuteAsync(invoiceId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(OperationResult<IEnumerable<InvoiceResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.ExecuteAsync();
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{invoiceId}/send-email")]
    [ProducesResponseType(typeof(OperationResult<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<string>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendInvoiceEmail(Guid invoiceId)
    {
        var result = await _sendInvoiceEmail.ExecuteAsync(invoiceId);
        return StatusCode(result.StatusCode, result);
    }
}