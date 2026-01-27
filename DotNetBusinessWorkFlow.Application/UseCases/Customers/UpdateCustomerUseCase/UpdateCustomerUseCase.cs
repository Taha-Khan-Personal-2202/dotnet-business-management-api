using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;

public class UpdateCustomerUseCase(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : IUpdateCustomerUseCase
{
    public async Task ExecuteAsync(Guid customerId, CustomerRequestUpdateDto request)
    {
        var customer = await customerRepository.GetByIdAsync(customerId)
            ?? throw new InvalidOperationException("Customer not found.");

        customer.Update(
            request.Name,
            request.Email,
            request.IsActive
        );

        await unitOfWork.SaveChangesAsync();
    }
}
