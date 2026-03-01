using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using FluentValidation;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;

public sealed class UpdateCustomerUseCase : IUpdateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CustomerRequestUpdateDto> _validator;

    public UpdateCustomerUseCase(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IValidator<CustomerRequestUpdateDto> validator)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<OperationResult<CustomerResponseDto>> ExecuteAsync(Guid customerId, CustomerRequestUpdateDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return OperationResult<CustomerResponseDto>.Error(errors, 400);
        }

        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer is null)
        {
            return OperationResult<CustomerResponseDto>.Error("Customer not found.", 404);
        }

        customer.Update(dto.Name, dto.Email, dto.IsActive);

        await _unitOfWork.SaveChangesAsync();

        var response = EntityToDtoMapping.MapCustomer(customer);
        return OperationResult<CustomerResponseDto>.Ok(response, "Customer updated successfully.");
    }
}