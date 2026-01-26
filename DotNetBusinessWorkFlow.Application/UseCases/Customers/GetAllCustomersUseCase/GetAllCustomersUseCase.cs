using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetAllCustomersUseCase;

public class GetAllCustomersUseCase(
    ICustomerRepository customerRepository
) : IGetAllCustomersUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<IEnumerable<CustomerResponseDto>> ExecuteAsync()
    {
        var customers = await _customerRepository.GetAllAsync();

        return customers
            .Select(EntityToDtoMapping.MapCustomer)
            .ToList();
    }

}
