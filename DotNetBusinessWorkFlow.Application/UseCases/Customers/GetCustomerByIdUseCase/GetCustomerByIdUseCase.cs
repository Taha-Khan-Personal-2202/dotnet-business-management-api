using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByIdUseCase;

public sealed class GetCustomerByIdUseCase : IGetCustomerByIdUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult<CustomerResponseDto?>> ExecuteAsync(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer is null)
        {
            return OperationResult<CustomerResponseDto?>.Error("Customer not found.", 404, null);
        }

        var dto = EntityToDtoMapping.MapCustomer(customer);
        return OperationResult<CustomerResponseDto?>.Ok(dto);
    }
}