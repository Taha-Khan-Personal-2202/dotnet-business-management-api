using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.GetPaymentByOrder;

public class GetPaymentUseCase(
    IPaymentRepository paymentRepository
) : IGetPaymentUseCase
{
    public async Task<OperationResult<PaymentResponseDto>> ExecuteAsync(Guid orderId)
    {
        var payment = await paymentRepository.GetByOrderIdAsync(orderId);
        if (payment == null)
        {
            return OperationResult<PaymentResponseDto>.Fail("Payment not found.", 404);
        }

        return OperationResult<PaymentResponseDto>.Succces(new PaymentResponseDto
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount.Amount,
            Status = payment.Status,
            CreatedAt = payment.CreatedAt
        }, "Payment fetched successfully.");
    }
}
