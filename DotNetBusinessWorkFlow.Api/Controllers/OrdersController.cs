using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.AddOrderItem;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.CancelOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.CompleteOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.ConfirmOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.CreateOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.GetAllOrders;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrderById;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrdersByCustomer;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.PayOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly ICreateOrderUseCase _createOrder;
    private readonly IAddOrderItemUseCase _addOrderItem;
    private readonly IConfirmOrderUseCase _confirmOrder;
    private readonly IPayOrderUseCase _payOrder;
    private readonly ICompleteOrderUseCase _completeOrder;
    private readonly ICancelOrderUseCase _cancelOrder;
    private readonly IGetOrderByIdUseCase _getOrderById;
    private readonly IGetAllOrdersUseCase _getAllOrders;
    private readonly IGetOrdersByCustomerUseCase _getOrdersByCustomer;

    public OrdersController(
        ICreateOrderUseCase createOrder,
        IAddOrderItemUseCase addOrderItem,
        IConfirmOrderUseCase confirmOrder,
        IPayOrderUseCase payOrder,
        ICompleteOrderUseCase completeOrder,
        ICancelOrderUseCase cancelOrder,
        IGetOrderByIdUseCase getOrderById,
        IGetAllOrdersUseCase getAllOrders,
        IGetOrdersByCustomerUseCase getOrdersByCustomer)
    {
        _createOrder = createOrder;
        _addOrderItem = addOrderItem;
        _confirmOrder = confirmOrder;
        _payOrder = payOrder;
        _completeOrder = completeOrder;
        _cancelOrder = cancelOrder;
        _getOrderById = getOrderById;
        _getAllOrders = getAllOrders;
        _getOrdersByCustomer = getOrdersByCustomer;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] OrderRequestDto dto)
    {
        var result = await _createOrder.ExecuteAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{orderId}/items")]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddItem(Guid orderId, [FromQuery] Guid productId, [FromQuery] int quantity)
    {
        var result = await _addOrderItem.ExecuteAsync(orderId, productId, quantity);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{orderId}/confirm")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Confirm(Guid orderId)
    {
        var result = await _confirmOrder.ExecuteAsync(orderId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{orderId}/pay")]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Pay(Guid orderId)
    {
        var result = await _payOrder.ExecuteAsync(orderId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{orderId}/complete")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Complete(Guid orderId)
    {
        var result = await _completeOrder.ExecuteAsync(orderId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{orderId}/cancel")]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(Guid orderId)
    {
        var result = await _cancelOrder.ExecuteAsync(orderId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{orderId}")]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OperationResult<OrderResponseDto?>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid orderId)
    {
        var result = await _getOrderById.ExecuteAsync(orderId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(OperationResult<IEnumerable<OrderResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllOrders.ExecuteAsync();
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("customer/{customerId}")]
    [ProducesResponseType(typeof(OperationResult<IEnumerable<OrderResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCustomer(Guid customerId)
    {
        var result = await _getOrdersByCustomer.ExecuteAsync(customerId);
        return StatusCode(result.StatusCode, result);
    }
}