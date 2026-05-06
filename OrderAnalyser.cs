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
        Console.WriteLine("New Orders:");
        foreach (var order in newOrders)
        Console.WriteLine($"  Id: {order.Id}, Status: {order.Status}, Amount: {order.Amount}");

        var totalShipped = orders.Where(o => o.Status == "Shipped").Sum(o => o.Amount);
        Console.WriteLine("Total Shipped Amount: " + totalShipped);

        var customerTotals = orders.GroupBy(o => o.CustomerId)
                                    .Select(g => new { CustomerId = g.Key, TotalOrders = g.Count() , TotalAmount = g.Sum(o => o.Amount)})
                                    .OrderByDescending(c => c.TotalAmount);
        foreach (var c in customerTotals)
            Console.WriteLine($"  Customer {c.CustomerId}: {c.TotalOrders} orders, £{c.TotalAmount}");

        var orderAbove400 = orders.Any(o => o.Amount > 400);
        Console.WriteLine("Any order above 400: " + orderAbove400);

        var highest = orders.OrderByDescending(o => o.Amount).FirstOrDefault();
        Console.WriteLine($"\nHighest: Id {highest?.Id}, Amount {highest?.Amount}");

        int linesWritten = OrderAnalyser.WriteOrdersToFile(orders);
        Console.WriteLine($"\nWrote {linesWritten} lines to orders.txt");
    }

    public static int WriteOrdersToFile(List<Order> orders)
    {
        int linesWritten = 0;
        using var writer = new StreamWriter("/Users/goncaloduque/Desktop/DotNetJourney/orders.txt");
        foreach (var order in orders)
        {
            writer.WriteLine($"ID: {order.Id}, Status: {order.Status}, Amount: {order.Amount}");
            linesWritten++;
        }
        return linesWritten;
    }

}