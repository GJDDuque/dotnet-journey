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
    public async Task<IActionResult> GetAll([FromQuery] string? status)
    {
        var orders = await _orderService.GetAll(status);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetById(id);
        return order is null ? 
            NotFound(new { Message = $"Order {id} not found." }) 
            : Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var order = await _orderService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderRequest request)
    {
        var updated = await _orderService.Update(id, request);
        return updated is null ?
            NotFound(new { Message = $"Order {id} not found." })
            : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _orderService.Delete(id);
        return deleted ? NoContent()
            : NotFound(new { Message = $"Order {id} not found." });
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var summary = await _orderService.GetSummary();
        return Ok(summary);
    }
}