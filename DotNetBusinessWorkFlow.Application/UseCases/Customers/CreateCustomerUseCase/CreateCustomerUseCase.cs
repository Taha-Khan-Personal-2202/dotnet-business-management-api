using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Application.Validators.Customers;
using DotNetBusinessWorkFlow.Application.DTOs.Common;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;

public class CreateCustomerUseCase(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    CreateCustomerValidator validations
) : ICreateCustomerUseCase
{
    public CreateCustomerValidator _validations = validations;

    public async Task<OperationResult<Guid>> ExecuteAsync(
        CustomerRequestDto customer,
        CancellationToken cancellationToken = default)
    {
        var validtionResult = await _validations.ValidateAsync(customer);
        if (!validtionResult.IsValid)
        {
            var errors = string.Join(";", validtionResult.Errors.Select(s => s.ErrorMessage + s.ErrorCode));
            return OperationResult<Guid>.Fail($"Validation failed: {errors}", 400);
        }

        var existing = await customerRepository.GetByEmailAsync(customer.Email);
        if (existing != null) return OperationResult<Guid>.Fail($"User with {existing.Email} already exist.", 409);

        var customerEntity = new Customer(customer.Name, customer.Email);

        await customerRepository.AddAsync(customerEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult<Guid>.Succces(customerEntity.Id, "Customer created succesfully.", 201);
    }
}
