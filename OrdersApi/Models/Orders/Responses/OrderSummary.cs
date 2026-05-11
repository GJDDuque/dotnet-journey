namespace OrdersApi.Models;

public record OrderSummary(
    int TotalOrders,
    decimal TotalAmount,
    IEnumerable<StatusSummary> StatusSummaries
);