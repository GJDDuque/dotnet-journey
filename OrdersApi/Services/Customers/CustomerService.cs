using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.Models;

namespace OrdersApi.Services;

public class CustomerService : ICustomerService
{
        private readonly OrdersDbContext _context;
    
    public CustomerService(OrdersDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAll()
        => await _context.Customers.ToListAsync();

    public async Task<Customer?> GetById(int id)
        => await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Order>> GetOrdersByCustomer(int id)
        => await _context.Orders.Where(o => o.CustomerId == id).ToListAsync();

    public async Task<Customer> Create(CreateCustomerRequest request)
    {
        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> Delete(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer is null)
            return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }
}