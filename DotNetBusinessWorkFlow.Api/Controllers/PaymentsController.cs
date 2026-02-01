using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using DotNetBusinessWorkFlow.Application.UseCases.Payments.CreatePayment;
using DotNetBusinessWorkFlow.Application.UseCases.Payments.GetPaymentByOrder;

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

    // PAY ORDER
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Pay([FromBody] PaymentRequestDto dto)
    {
        var result = await _createPayment.ExecuteAsync(dto);
        return Ok(result);
    }

    // GET PAYMENT BY ORDER
    [HttpGet("order/{orderId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetByOrder(Guid orderId)
    {
        var result = await _getByOrder.ExecuteAsync(orderId);
        return result == null ? NotFound() : Ok(result);
    }
}
