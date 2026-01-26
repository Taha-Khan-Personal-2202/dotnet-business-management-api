using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByIdUseCase;

public class GetCustomerByIdUseCase(
    ICustomerRepository customerRepository
) : IGetCustomerByIdUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerResponseDto?> ExecuteAsync(Guid id)
    {
        return EntityToDtoMapping.MapCustomer(await _customerRepository.GetByIdAsync(id));
    }
}
