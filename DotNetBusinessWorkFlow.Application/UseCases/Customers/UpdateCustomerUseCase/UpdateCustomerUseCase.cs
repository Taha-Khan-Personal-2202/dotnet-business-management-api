using DotNetBusinessWorkFlow.Application.Common.Interfaces;
using DotNetBusinessWorkFlow.Application.DTOs.Common;
using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Domain.Interfaces;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;

public class UpdateCustomerUseCase(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : IUpdateCustomerUseCase
{
    public async Task<OperationResult<bool>> ExecuteAsync(Guid customerId, CustomerRequestUpdateDto request)
    {
        var customer = await customerRepository.GetByIdAsync(customerId);
        if (customer is null)
        {
            return OperationResult<bool>.Fail("Customer not found.", 404);
        }

        customer.Update(
            request.Name,
            request.Email,
            request.IsActive
        );

        await unitOfWork.SaveChangesAsync();
        return OperationResult<bool>.Succces(true, "Customer updated successfully.");
    }
}
