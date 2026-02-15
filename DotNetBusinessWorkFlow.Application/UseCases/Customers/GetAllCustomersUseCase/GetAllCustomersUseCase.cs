using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.Mappings;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetAllCustomersUseCase;

public sealed class GetAllCustomersUseCase : IGetAllCustomersUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult<IEnumerable<CustomerResponseDto>>> ExecuteAsync()
    {
        var customers = await _customerRepository.GetAllAsync();

        var dtos = customers
            .Select(EntityToDtoMapping.MapCustomer)
            .ToList();

        return OperationResult<IEnumerable<CustomerResponseDto>>.Ok(dtos);
    }
}