using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Payments;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.GetPaymentByOrder;

public interface IGetPaymentUseCase
{
    Task<OperationResult<PaymentResponseDto>> ExecuteAsync(Guid orderId);
}
