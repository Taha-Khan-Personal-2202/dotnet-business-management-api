using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Auth;
using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using DotNetBusinessWorkFlow.Application.Validators.Payments;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.CreatePayment;

public class CreatePaymentUseCase(
    IPaymentRepository paymentRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork,
    IValidator<PaymentRequestDto> validator) : ICreatePaymentUseCase
{
    public IValidator<PaymentRequestDto> _validator { get; } = validator;

    public async Task<OperationResult<PaymentResponseDto>> ExecuteAsync(PaymentRequestDto dto)
    {
        // validiton
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<PaymentResponseDto>.Error(errors, 400);
        }

        // finding order
        var order = await orderRepository.GetByIdAsync(dto.OrderId);
        if (order == null)
            return OperationResult<PaymentResponseDto>.Error("Order not found.");

        // checking status
        if (order.Status != OrderStatus.Confirmed)
            return OperationResult<PaymentResponseDto>.Error("Only confirmed orders can be paid.");

        // checking payment
        var existingPayment = await paymentRepository.GetByOrderIdAsync(dto.OrderId);
        if (existingPayment != null)
            return OperationResult<PaymentResponseDto>.Error("Payment already exists.");

        // adding payment and updating status
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

        return OperationResult<PaymentResponseDto>.Ok(response);
    }
}
