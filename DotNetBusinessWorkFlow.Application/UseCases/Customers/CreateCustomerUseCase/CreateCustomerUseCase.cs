using DotNetBusinessWorkflow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;

public class CreateCustomerUseCase(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> ExecuteAsync(CustomerRequestDto customer, CancellationToken cancellationToken = default)
    {
        var customerEntity = new Customer(customer.Name, customer.Email);
        await _customerRepository.AddAsync(customerEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
