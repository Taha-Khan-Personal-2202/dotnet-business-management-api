using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByEmailUseCase;

public class GetCustomerByEmailUseCase(
    ICustomerRepository customerRepository
) : IGetCustomerByEmailUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerResponseDto?> ExecuteAsync(string email)
    {
        return EntityToDtoMapping.MapCustomerEntityToDto(await _customerRepository.GetByEmailAsync(email));
    }
}
