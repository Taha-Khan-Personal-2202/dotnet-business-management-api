using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.UseCases.Products.CreateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.GetProductByIdUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.GetAllProductsUseCase;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/products")]
[Authorize] // Base authorization
public class ProductsController : ControllerBase
{
    private readonly ICreateProductUseCase _createProduct;
    private readonly IUpdateProductUseCase _updateProduct;
    private readonly IDeactivateProductUseCase _deactivateProduct;
    private readonly IGetProductByIdUseCase _getById;
    private readonly IGetAllProductsUseCase _getAll;

    public ProductsController(
        ICreateProductUseCase createProduct,
        IUpdateProductUseCase updateProduct,
        IDeactivateProductUseCase deactivateProduct,
        IGetProductByIdUseCase getById,
        IGetAllProductsUseCase getAll)
    {
        _createProduct = createProduct;
        _updateProduct = updateProduct;
        _deactivateProduct = deactivateProduct;
        _getById = getById;
        _getAll = getAll;
    }

    // CREATE PRODUCT
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] ProductRequestDto dto)
    {
        var result = await _createProduct.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { productId = result }, result);
    }

    // UPDATE PRODUCT
    [HttpPut("{productId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid productId, [FromBody] ProductRequestUpdateDto dto)
    {
        await _updateProduct.ExecuteAsync(dto);
        return Ok();
    }

    // DEACTIVATE PRODUCT
    [HttpPatch("{productId}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate(Guid productId)
    {
        await _deactivateProduct.ExecuteAsync(productId);
        return NoContent();
    }

    // GET BY ID
    [HttpGet("{productId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetById(Guid productId)
    {
        var result = await _getById.ExecuteAsync(productId);
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
