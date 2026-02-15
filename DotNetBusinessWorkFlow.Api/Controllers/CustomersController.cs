using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.DeactivateCustomerUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetAllCustomersUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByIdUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/customers")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly ICreateCustomerUseCase _createCustomer;
    private readonly IUpdateCustomerUseCase _updateCustomer;
    private readonly IDeactivateCustomerUseCase _deactivateCustomer;
    private readonly IGetCustomerByIdUseCase _getById;
    private readonly IGetAllCustomersUseCase _getAll;

    public CustomersController(
        ICreateCustomerUseCase createCustomer,
        IUpdateCustomerUseCase updateCustomer,
        IDeactivateCustomerUseCase deactivateCustomer,
        IGetCustomerByIdUseCase getById,
        IGetAllCustomersUseCase getAll)
    {
        _createCustomer = createCustomer;
        _updateCustomer = updateCustomer;
        _deactivateCustomer = deactivateCustomer;
        _getById = getById;
        _getAll = getAll;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CustomerRequestDto dto)
    {
        var result = await _createCustomer.ExecuteAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{customerId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid customerId, [FromBody] CustomerRequestUpdateDto dto)
    {
        var result = await _updateCustomer.ExecuteAsync(customerId, dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPatch("{customerId}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate(Guid customerId)
    {
        var result = await _deactivateCustomer.ExecuteAsync(customerId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{customerId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetById(Guid customerId)
    {
        var result = await _getById.ExecuteAsync(customerId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.ExecuteAsync();
        return StatusCode(result.StatusCode, result);
    }
}