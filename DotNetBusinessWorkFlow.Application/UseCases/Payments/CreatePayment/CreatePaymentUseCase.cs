using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Payments;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Enums;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.UseCases.Payments.CreatePayment;

public sealed class CreatePaymentUseCase : ICreatePaymentUseCase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<PaymentRequestDto> _validator;

    public CreatePaymentUseCase(
        IPaymentRepository paymentRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IValidator<PaymentRequestDto> validator)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<OperationResult<PaymentResponseDto>> ExecuteAsync(PaymentRequestDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<PaymentResponseDto>.Error(errors, 400);
        }

        var order = await _orderRepository.GetByIdAsync(dto.OrderId);
        if (order is null)
            return OperationResult<PaymentResponseDto>.Error("Order not found.", 404);

        if (order.Status != OrderStatus.Confirmed)
            return OperationResult<PaymentResponseDto>.Error("Only confirmed orders can be paid.", 400);

        var existingPayment = await _paymentRepository.GetByOrderIdAsync(dto.OrderId);
        if (existingPayment is not null)
            return OperationResult<PaymentResponseDto>.Error("A payment already exists for this order.", 409);

        var payment = new Payment(dto.OrderId, dto.Amount);
        payment.MarkAsPaid();

        await _paymentRepository.AddAsync(payment);
        order.MarkAsPaid();

        await _unitOfWork.SaveChangesAsync();

        var response = new PaymentResponseDto
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount.Amount,
            Status = payment.Status,
            CreatedAt = payment.CreatedAt
        };

        return OperationResult<PaymentResponseDto>.Ok(response, "Payment recorded successfully.", 201);
    }
}