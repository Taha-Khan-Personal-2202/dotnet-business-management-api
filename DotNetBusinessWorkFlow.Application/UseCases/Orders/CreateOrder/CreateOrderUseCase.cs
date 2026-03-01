using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.UseCases.Orders.CreateOrder;

public sealed class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<OrderRequestDto> _validator;

    public CreateOrderUseCase(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IValidator<OrderRequestDto> validator)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<OperationResult<OrderResponseDto>> ExecuteAsync(OrderRequestDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<OrderResponseDto>.Error(errors, 400);
        }

        var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
        if (customer is null)
        {
            return OperationResult<OrderResponseDto>.Error("Customer not found.", 404);
        }

        if (!customer.IsActive)
        {
            return OperationResult<OrderResponseDto>.Error("Cannot create order: customer account is inactive.", 403);
        }

        var order = new Order(dto.CustomerId);

        await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        var response = EntityToDtoMapping.MapOrder(order);
        return OperationResult<OrderResponseDto>.Ok(response, "Order created successfully.", 201);
    }
}