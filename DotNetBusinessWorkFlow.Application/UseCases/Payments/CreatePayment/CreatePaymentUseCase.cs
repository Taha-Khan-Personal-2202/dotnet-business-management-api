using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.CreatePayment;

public class CreatePaymentUseCase(
    IPaymentRepository paymentRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork
) : ICreatePaymentUseCase
{
    public async Task<PaymentResponseDto> ExecuteAsync(PaymentRequestDto dto)
    {
        var order = await orderRepository.GetByIdAsync(dto.OrderId)
            ?? throw new Exception("Order not found.");

        if (order.Status != Domain.Enums.OrderStatus.Confirmed)
            throw new Exception("Only confirmed orders can be paid.");

        var existingPayment = await paymentRepository.GetByOrderIdAsync(dto.OrderId);
        if (existingPayment != null)
            throw new Exception("Payment already exists.");

        var payment = new Payment(dto.OrderId, dto.Amount);
        payment.MarkAsPaid();

        await paymentRepository.AddAsync(payment);
        order.MarkAsPaid();

        await unitOfWork.SaveChangesAsync();

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
