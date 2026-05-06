public class OrderExplorer
{
    record Order(int Id, string Status, decimal Amount, string? Notes);

    public class CreateOrderRequest
    {
       public required int Id { get; init; }
       public required string Status { get; init; }
       public required decimal Amount { get; init; }
       public string? Notes { get; init; }
    }

    public static void Main()
    {
        var orders = new List<Order>
        {
            new(1, "New",      1500, "First order"),
            new(2, "Shipped",  300, "Second order"),
            new(3, "New",       755, null),
            new(4, "Shipped",  5000, "Fourth order"),
            new(5, "Cancelled", 90, null),
            new(6, "New",      2000, "Sixth order"),
        };

        var classifiedOrders = orders.Select(o => (Order: o, Classification: new OrderExplorer().ClassifyOrder(o)));
        foreach (var (order, classification) in classifiedOrders)
            Console.WriteLine($"Order {order.Id}: {classification}");

        var newOrderRequest = new OrderExplorer().CreateOrder(id: 7,
            status: "New",
            amount: 1200,
            notes: "Seventh order"
        );
        Console.WriteLine($"Created order request: Id {newOrderRequest.Id}, Status {newOrderRequest.Status}, Amount {newOrderRequest.Amount}, Notes: {newOrderRequest.Notes}");

        var shipOrder = new OrderExplorer().MarkAsShipped(orders[3]);
        Console.WriteLine($"Order {orders[3].Id} marked as: {shipOrder.Status}");
    }

    string ClassifyOrder(Order order)
    {
        return order switch
        {
            null => "Invalid",
            { Status: "Cancelled" } => "Cancelled",
            { Amount: > 1000, Status: "New" } => "High Priority New",
            { Amount: > 1000 } => "High Value",
            { Status: "New" } => "Standard New",
            _ => "General"
        };
    }

    public CreateOrderRequest CreateOrder(int id, string status, decimal amount, string? notes)
    {
        return new CreateOrderRequest
        {
            Id = id,
            Status = status,
            Amount = amount,
            Notes = notes
        };
    }

    Order MarkAsShipped(Order order)
    {
        return order with { Status = "Shipped" };
    }



}