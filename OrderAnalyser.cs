public class OrderAnalyser
{

    public record Order(int Id, string Status, decimal Amount, int CustomerId);

    public static void Main()
    {
        var orders = new List<Order>
        {
            new(1, "New",      150m, 10),
            new(2, "Shipped",  300m, 10),
            new(3, "New",       75m, 20),
            new(4, "Shipped",  500m, 20),
            new(5, "Cancelled", 90m, 10),
            new(6, "New",      200m, 30),
        };

        var newOrders = orders.Where(o => o.Status == "New").OrderByDescending(o => o.Amount);
        Console.WriteLine("New Orders: " + newOrders.ToList());

        var totalShipped = orders.Where(o => o.Status == "Shipped").Sum(o => o.Amount);
        Console.WriteLine("Total Shipped Amount: " + totalShipped);

        var customerTotals = orders.GroupBy(o => o.CustomerId)
                                    .Select(g => new { CustomerId = g.Key, TotalOrders = g.Count() , TotalAmount = g.Sum(o => o.Amount)})
                                    .OrderByDescending(c => c.TotalAmount);
        Console.WriteLine("Customer Totals: " + customerTotals.ToList());

        var orderAbove400 = orders.Any(o => o.Amount > 400);
        Console.WriteLine("Any order above 400: " + orderAbove400);

        var highestOrder = orders.OrderByDescending(o => o.Amount).FirstOrDefault();
        Console.WriteLine("Highest Order: " + highestOrder);
    }

    public int WriteOrdersToFile(List<Order> orders)
    {
        var filePath = "orders.txt";
        using var writer = new StreamWriter(filePath);
        foreach (var order in orders)
        {
            writer.WriteLine($"Order ID: {order.Id}, Status: {order.Status}, Amount: {order.Amount}");
        }
        return orders.Count;
    }

}