using Microsoft.AspNetCore.Mvc;
using OrdersApi.Models;
using OrdersApi.Services;

namespace OrdersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] string? status)
    {
        var orders = _orderService.GetAll(status);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var order = _orderService.GetById(id);
        return order is null ? 
            NotFound(new { Message = $"Order {id} not found." }) 
            : Ok(order);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateOrderRequest request)
    {
        var order = _orderService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = order.id }, order);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOrderRequest(int id, [FromBody] UpdateOrderRequest request)
    {
        var updated = _orderService.Update(id, request);
        return updated is null ?
            NotFound(new { Message = $"Order {id} not found." })
            : Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _orderService.Delete(id);
        return deleted ? NoContent()
            : NotFound(new { Message = $"Order {id} not found." });
    }

    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        var summary = _orderService.GetSummary();
        return Ok(summary);
    }
}