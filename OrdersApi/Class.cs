public record CreateOrderRequest(string CustomerName, decimal Amount);

public record UpdateOrderRequest(string Status);