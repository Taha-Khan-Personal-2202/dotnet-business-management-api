using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Payments;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.CreatePayment;

public interface ICreatePaymentUseCase
{
    Task<OperationResult<PaymentResponseDto>> ExecuteAsync(PaymentRequestDto dto);
}
