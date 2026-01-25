using DotNetBusinessWorkFlow.Application.DTOs.Customers;

namespace DotNetBusinessWorkFlow.Application.UseCases.Customers.GetAllCustomersUseCase;

public interface IGetAllCustomersUseCase
{
    Task<IEnumerable<CustomerResponseDto>> ExecuteAsync();
}
