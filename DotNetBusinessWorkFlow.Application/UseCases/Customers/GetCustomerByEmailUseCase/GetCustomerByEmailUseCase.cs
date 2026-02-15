using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByEmailUseCase;

public class GetCustomerByEmailUseCase(
    ICustomerRepository customerRepository
) : IGetCustomerByEmailUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<OperationResult<CustomerResponseDto?>> ExecuteAsync(string email)
    {
        var customer = await _customerRepository.GetByEmailAsync(email);
        if (customer == null)
        {
            return OperationResult<CustomerResponseDto?>.Error("Customer not found.", 404, null);
        }

        var dto = EntityToDtoMapping.MapCustomer(customer);
        return OperationResult<CustomerResponseDto?>.Ok(dto);
    }
}
