using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? status)
    {
        var orders = new[]
        {
            new { Id = 1, Status = "New", Amount = 150m },
            new { Id = 2, Status = "Shipped", Amount = 300m },
            new { Id = 3, Status = "New", Amount = 75m }
        };

        if (!string.IsNullOrWhiteSpace(status))
            orders = orders.Where(o => o.Status == status).ToArray();

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id){
        
        if(id != 1)
            return NotFound(new { Message = $"Order {id} not found." });

        return Ok(new { Id = 1, Status = "New", Amount = 150m });
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateOrderRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerName))
            return BadRequest(new { Message = "Customer name is required." });

        var created = new { Id = 99, CustomerName = request.CustomerName, Amount = request.Amount, Status = "New" };

        return CreatedAtAction(nameof(GetById), new { id = 99 }, created);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOrderRequest(int id, string Status)
    {
        if(id != 1 && id != 2)
            return NotFound(new { Message = $"Order {id} not found." });
        if (string.IsNullOrWhiteSpace(Status))
            return BadRequest(new { Message = "Status is required." });

        return Ok(new { Id = id, Status = Status});
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if(id != 1 && id != 2)
            return NotFound(new { Message = $"Order {id} not found." });


        return NoContent();
    }
}