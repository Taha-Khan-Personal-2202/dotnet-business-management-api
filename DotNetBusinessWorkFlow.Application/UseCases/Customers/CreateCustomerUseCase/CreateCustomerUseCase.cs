using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;

public sealed class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CustomerRequestDto> _validator;

    public CreateCustomerUseCase(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IValidator<CustomerRequestDto> validator)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<OperationResult<Guid>> ExecuteAsync(CustomerRequestDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<Guid>.Error($"Validation failed: {errors}", 400);
        }

        var existing = await _customerRepository.GetByEmailAsync(dto.Email);
        if (existing is not null)
        {
            return OperationResult<Guid>.Error($"Customer with email {dto.Email} already exists.", 409);
        }

        var customer = new Customer(dto.Name, dto.Email);

        await _customerRepository.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        return OperationResult<Guid>.Ok(customer.Id, "Customer created successfully.", 201);
    }
}