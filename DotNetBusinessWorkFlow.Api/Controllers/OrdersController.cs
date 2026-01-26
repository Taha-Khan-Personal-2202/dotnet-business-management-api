using Microsoft.AspNetCore.Mvc;
using DotNetBusinessWorkFlow.Application.DTOs.Orders;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.CreateOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.AddOrderItem;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.ConfirmOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.PayOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.CompleteOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.CancelOrder;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrderById;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.GetAllOrders;
using DotNetBusinessWorkFlow.Application.UseCases.Orders.GetOrdersByCustomer;

namespace DotNetBusinessWorkFlow.API.Controllers;

[ApiController]
[Route("api/orders")]
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

    // CREATE ORDER
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderRequestDto dto)
    {
        var result = await _createOrder.ExecuteAsync(dto);
        return CreatedAtAction(nameof(GetById), new { orderId = result.Id }, result);
    }

    // ADD ITEM
    [HttpPost("{orderId}/items")]
    public async Task<IActionResult> AddItem(
        Guid orderId,
        [FromQuery] Guid productId,
        [FromQuery] int quantity)
    {
        var result = await _addOrderItem.ExecuteAsync(orderId, productId, quantity);
        return Ok(result);
    }

    // CONFIRM ORDER
    [HttpPost("{orderId}/confirm")]
    public async Task<IActionResult> Confirm(Guid orderId)
    {
        var result = await _confirmOrder.ExecuteAsync(orderId);
        return Ok(result);
    }

    // PAY ORDER
    [HttpPost("{orderId}/pay")]
    public async Task<IActionResult> Pay(Guid orderId)
    {
        var result = await _payOrder.ExecuteAsync(orderId);
        return Ok(result);
    }

    // COMPLETE ORDER
    [HttpPost("{orderId}/complete")]
    public async Task<IActionResult> Complete(Guid orderId)
    {
        var result = await _completeOrder.ExecuteAsync(orderId);
        return Ok(result);
    }

    // CANCEL ORDER
    [HttpPost("{orderId}/cancel")]
    public async Task<IActionResult> Cancel(Guid orderId)
    {
        var result = await _cancelOrder.ExecuteAsync(orderId);
        return Ok(result);
    }

    // GET BY ID
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetById(Guid orderId)
    {
        var result = await _getOrderById.ExecuteAsync(orderId);
        return result == null ? NotFound() : Ok(result);
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllOrders.ExecuteAsync();
        return Ok(result);
    }

    // GET BY CUSTOMER
    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomer(Guid customerId)
    {
        var result = await _getOrdersByCustomer.ExecuteAsync(customerId);
        return Ok(result);
    }
}
