using DotNetBusinessWorkFlow.Application.DTOs.Payments;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.CreatePayment;

public interface ICreatePaymentUseCase
{
    Task<PaymentResponseDto> ExecuteAsync(PaymentRequestDto dto);
}