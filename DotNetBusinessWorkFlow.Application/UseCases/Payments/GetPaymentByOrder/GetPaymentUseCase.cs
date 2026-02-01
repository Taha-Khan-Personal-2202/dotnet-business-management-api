using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.GetPaymentByOrder;

public class GetPaymentUseCase(
    IPaymentRepository paymentRepository
) : IGetPaymentUseCase
{
    public async Task<PaymentResponseDto?> ExecuteAsync(Guid orderId)
    {
        var payment = await paymentRepository.GetByOrderIdAsync(orderId);
        if (payment == null) return null;

        return new PaymentResponseDto
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount.Amount,
            Status = payment.Status,
            CreatedAt = payment.CreatedAt
        };
    }
}
