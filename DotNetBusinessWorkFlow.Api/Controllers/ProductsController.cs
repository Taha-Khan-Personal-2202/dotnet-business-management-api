using DotNetBusinessWorkFlow.Application.DTOs.Products;
using DotNetBusinessWorkFlow.Application.UseCases.Products.CreateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.DeactivateProductUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.GetAllProductsUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.GetProductByIdUseCase;
using DotNetBusinessWorkFlow.Application.UseCases.Products.UpdateProductUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/products")]
[Authorize]
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

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(OperationResult<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(OperationResult<Guid>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ProductRequestDto dto)
    {
        var result = await _createProduct.ExecuteAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{productId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(OperationResult<ProductResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<ProductResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<ProductResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid productId, [FromBody] ProductRequestUpdateDto dto)
    {
        dto.Id = productId;
        var result = await _updateProduct.ExecuteAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPatch("{productId}/deactivate")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(OperationResult<ProductResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<ProductResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(Guid productId)
    {
        var result = await _deactivateProduct.ExecuteAsync(productId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{productId}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(OperationResult<ProductResponseDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<ProductResponseDto?>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid productId)
    {
        var result = await _getById.ExecuteAsync(productId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(OperationResult<IEnumerable<ProductResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAll.ExecuteAsync();
        return StatusCode(result.StatusCode, result);
    }
}