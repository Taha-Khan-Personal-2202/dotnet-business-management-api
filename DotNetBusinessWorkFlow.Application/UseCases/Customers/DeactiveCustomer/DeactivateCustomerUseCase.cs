// DeactivateCustomerUseCase.cs
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.DeactivateCustomerUseCase;

public sealed class DeactivateCustomerUseCase : IDeactivateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateCustomerUseCase(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult<CustomerResponseDto>> ExecuteAsync(Guid customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer is null)
        {
            return OperationResult<CustomerResponseDto>.Error("Customer not found.", 404);
        }

        customer.Deactivate();

        await _unitOfWork.SaveChangesAsync();

        var customerDto = EntityToDtoMapping.MapCustomer(customer);
        return OperationResult<CustomerResponseDto>.Ok(customerDto, "Customer deactivated successfully.");
    }
}