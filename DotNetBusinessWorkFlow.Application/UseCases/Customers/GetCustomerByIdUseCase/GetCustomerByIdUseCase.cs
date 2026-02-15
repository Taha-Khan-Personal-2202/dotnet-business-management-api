using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByIdUseCase;

public class GetCustomerByIdUseCase(
    ICustomerRepository customerRepository
) : IGetCustomerByIdUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<OperationResult<CustomerResponseDto>> ExecuteAsync(Guid id)
    {
        var customer = EntityToDtoMapping.MapCustomer(await _customerRepository.GetByIdAsync(id));
        return customer is null
            ? OperationResult<CustomerResponseDto>.Fail("Customer not found.", 404)
            : OperationResult<CustomerResponseDto>.Succces(customer, "Customer fetched successfully.");
    }
}
