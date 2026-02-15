using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.CreatePayment;

public class CreatePaymentUseCase(
    IPaymentRepository paymentRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork
) : ICreatePaymentUseCase
{
    public async Task<OperationResult<PaymentResponseDto>> ExecuteAsync(PaymentRequestDto dto)
    {
        var order = await orderRepository.GetByIdAsync(dto.OrderId);
        if (order is null)
        {
            return OperationResult<PaymentResponseDto>.Fail("Order not found.", 404);
        }

        if (order.Status != OrderStatus.Confirmed)
        {
            return OperationResult<PaymentResponseDto>.Fail("Only confirmed orders can be paid.", 400);
        }

        var existingPayment = await paymentRepository.GetByOrderIdAsync(dto.OrderId);
        if (existingPayment != null)
        {
            return OperationResult<PaymentResponseDto>.Fail("Payment already exists.", 409);
        }

        var payment = new Payment(dto.OrderId, dto.Amount);
        payment.MarkAsPaid();

        await paymentRepository.AddAsync(payment);
        order.MarkAsPaid();

        await unitOfWork.SaveChangesAsync();

        var response = new PaymentResponseDto
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount.Amount,
            Status = payment.Status,
            CreatedAt = payment.CreatedAt
        };

        return OperationResult<PaymentResponseDto>.Succces(response, "Payment created successfully.", 201);
    }
}
