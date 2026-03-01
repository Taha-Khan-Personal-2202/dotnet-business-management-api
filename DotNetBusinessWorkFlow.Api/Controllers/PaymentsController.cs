using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using DotNetBusinessWorkFlow.Application.UseCases.Payments.CreatePayment;
using DotNetBusinessWorkFlow.Application.UseCases.Payments.GetPaymentByOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/payments")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly ICreatePaymentUseCase _createPayment;
    private readonly IGetPaymentUseCase _getByOrder;

    public PaymentsController(
        ICreatePaymentUseCase createPayment,
        IGetPaymentUseCase getByOrder)
    {
        _createPayment = createPayment;
        _getByOrder = getByOrder;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(OperationResult<PaymentResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(OperationResult<PaymentResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<PaymentResponseDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(OperationResult<PaymentResponseDto>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto dto)
    {
        var result = await _createPayment.ExecuteAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("order/{orderId}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(OperationResult<PaymentResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<PaymentResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByOrder(Guid orderId)
    {
        var result = await _getByOrder.ExecuteAsync(orderId);
        return StatusCode(result.StatusCode, result);
    }
}