using DotNetBusinessWorkFlow.Domain.Interfaces;
using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;

public class UpdateCustomerUseCase(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : IUpdateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(CustomerRequestUpdateDto request)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id)
            ?? throw new InvalidOperationException("Customer not found.");


        await _customerRepository.UpdateAsync(customer);
        await _unitOfWork.SaveChangesAsync();
    }
}
