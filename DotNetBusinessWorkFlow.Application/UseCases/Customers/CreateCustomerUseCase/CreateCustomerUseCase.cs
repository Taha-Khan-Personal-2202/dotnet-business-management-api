using DotNetBusinessWorkFlow.Domain.Entities;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;

public class CreateCustomerUseCase(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : ICreateCustomerUseCase
{
    public async Task<Guid> ExecuteAsync(
        CustomerRequestDto customer,
        CancellationToken cancellationToken = default)
    {
        var existing = await customerRepository.GetByEmailAsync(customer.Email);
        if (existing != null)
            throw new InvalidOperationException("Customer with this email already exists.");

        var customerEntity = new Customer(customer.Name, customer.Email);

        await customerRepository.AddAsync(customerEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return customerEntity.Id;
    }
}
