using DotNetBusinessWorkFlow.Application.DTOs.Customers;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.CreateCustomerUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetAllCustomersUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.GetCustomerByIdUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Customers.UpdateCustomerUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/customers")]
[Authorize] // Base authorization
public class CustomersController : ControllerBase
{
    private readonly ICreateCustomerUseCase _createCustomer;
    private readonly IUpdateCustomerUseCase _updateCustomer;
    private readonly IGetCustomerByIdUseCase _getById;
    private readonly IGetAllCustomersUseCase _getAll;

    public CustomersController(
        ICreateCustomerUseCase createCustomer,
        IUpdateCustomerUseCase updateCustomer,
        IGetCustomerByIdUseCase getById,
        IGetAllCustomersUseCase getAll)
    {
        _createCustomer = createCustomer;
        _updateCustomer = updateCustomer;
        _getById = getById;
        _getAll = getAll;
    }

    // CREATE CUSTOMER
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CustomerRequestDto dto)
    {
        var result = await _createCustomer.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { customerId = result }, result);
    }

    // UPDATE CUSTOMER
    [HttpPut("{customerId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid customerId, [FromBody] CustomerRequestUpdateDto dto)
    {
        await _updateCustomer.ExecuteAsync(dto);
        return Ok();
    }

    // DEACTIVATE CUSTOMER
    [HttpPatch("{customerId}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate(Guid customerId)
    {
        //await _deactivateCustomer.ExecuteAsync(customerId);
        return NoContent();
    }

    // GET BY ID
    [HttpGet("{customerId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetById(Guid customerId)
    {
        var result = await _getById.ExecuteAsync(customerId);
        return result == null ? NotFound() : Ok(result);
    }

    // GET ALL
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.ExecuteAsync();
        return Ok(result);
    }
}
