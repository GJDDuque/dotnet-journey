using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;
using OrdersApi.Models;

namespace OrdersApi.Services;

public class OrderService : IOrderService
{
    private readonly OrdersDbContext _context;
    
    public OrderService(OrdersDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAll(string? status)
    {
        var query = _context.Orders.AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(o => o.Status == status);

        return await query.ToListAsync();
    }

    public async Task<Order?> GetById(int id)
        => await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

    public async Task<Order> Create(CreateOrderRequest request)
    {
        var order = new Order
        {
            CustomerName = request.CustomerName,
            Amount = request.Amount,
            Status = "New",
            CreatedAt = DateTime.UtcNow
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> Update(int id, UpdateOrderRequest request)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order is null) return null;

        order.Status = request.Status;
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> Delete(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order is null) return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<OrderSummary> GetSummary()
    {
        var orders = await _context.Orders.ToListAsync();

        var statusSummaries = orders
            .GroupBy(o => o.Status)
            .Select(g => new StatusSummary(g.Key, g.Count(), g.Sum(o => o.Amount)))
            .ToList();

        return new OrderSummary(
            orders.Count,
            orders.Sum(o => o.Amount),
            statusSummaries
        );
    }
}