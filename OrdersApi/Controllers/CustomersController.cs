using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersApi.Models;
using OrdersApi.Services;

namespace OrdersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAll();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetById(id);
        return customer is null ?
            NotFound(new { Message = $"Customer {id} not found." })
            : Ok(customer);
    }

    [HttpGet("{id}/orders")]
    public async Task<IActionResult> GetOrdersByCustomer(int id)
    {
        var customer = await _customerService.GetById(id);
        if (customer is null)
            return NotFound(new { Message = $"Customer {id} not found." });

        var orders = await _customerService.GetOrdersByCustomer(id);
        return Ok(orders); // Empty array is a valid 200 response
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
        var customer = await _customerService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }    

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _customerService.Delete(id);
        return deleted ? NoContent() : NotFound(new { Message = $"Customer {id} not found." });
    }
}