using DotNetBusinessWorkFlow.Application.DTOs.Payments;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.GetPaymentByOrder;

public interface IGetPaymentUseCase
{
    Task<PaymentResponseDto?> ExecuteAsync(Guid orderId);
}