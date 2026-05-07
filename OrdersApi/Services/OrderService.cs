using OrdersApi.Models;

namespace OrdersApi.Services;

public class OrderService : IOrderService
{
    private static readonly List<Order> _orders = new()
    {
        new Order { id = 1, Amount = 150m, Status = "New" },
        new Order { id = 2, Amount = 300m, Status = "Shipped" },
        new Order { id = 3, Amount = 75m, Status = "New" }
    };

    public IEnumerable<Order> GetAll(string? status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return _orders;

        return _orders.Where(o => o.Status == status);
    }

    public Order? GetById(int id) => _orders.FirstOrDefault(o => o.id == id);

    public Order Create(CreateOrderRequest resquest)
    {
        var order = new Order
        {
            id = _orders.Max(o => o.id) + 1,
            CustomerName = resquest.CustomerName,
            Amount = resquest.Amount,
            Status = "New"
        };
        _orders.Add(order);
        return order;
    }

    public Order? Update(int id, UpdateOrderRequest request)
    {
        var order = _orders.FirstOrDefault(o => o.id == id);
        if (order is null) return null;

        order.Status = request.Status;
        return order;
    }

    public bool Delete(int id)
    {
        var order = _orders.FirstOrDefault(o => o.id == id);
        if (order is null) return false;

        _orders.Remove(order);
        return true;
    }

    public OrderSummary GetSummary()
    {
        var summary = _orders.GroupBy(o => o.Status)
            .Select(g => new StatusSummary 
            { 
                Status = g.Key, 
                Count = g.Count(), 
                Amount = g.Sum(o => o.Amount) 
            })
            .ToList();
    

        return new OrderSummary
        {
            TotalOrders = _orders.Count,
            TotalAmount = _orders.Sum(o => o.Amount),
            StatusSummaries = summary
        };
    }
}