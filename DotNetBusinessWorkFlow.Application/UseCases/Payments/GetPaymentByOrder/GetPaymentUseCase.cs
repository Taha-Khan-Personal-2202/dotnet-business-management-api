using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.GetPaymentByOrder;

public sealed class GetPaymentUseCase : IGetPaymentUseCase
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentUseCase(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<OperationResult<PaymentResponseDto>> ExecuteAsync(Guid orderId)
    {
        var payment = await _paymentRepository.GetByOrderIdAsync(orderId);
        if (payment is null)
            return OperationResult<PaymentResponseDto>.Error("No payment found for this order.", 404);

        var response = new PaymentResponseDto
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount.Amount,
            Status = payment.Status,
            CreatedAt = payment.CreatedAt
        };

        return OperationResult<PaymentResponseDto>.Ok(response, "Payment details retrieved successfully.");
    }
}