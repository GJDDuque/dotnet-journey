namespace OrdersApi.Models;

public class Order
{
    public int id { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string CustomerName { get; set; } = string.Empty;
}

public class StatusSummary
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Amount { get; set; }
}

public class OrderSummary
{
    public int TotalOrders { get; set; }
    public decimal TotalAmount { get; set; }
    public IEnumerable<StatusSummary> StatusSummaries { get; set; } = Enumerable.Empty<StatusSummary>();

}