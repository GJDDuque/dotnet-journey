using OrdersApi.Models;

namespace OrdersApi.Services;

public interface IOrderService
{
    IEnumerable<Order> GetAll(string? status);
    Order? GetById(int id);
    Order Create(CreateOrderRequest request);
    Order? Update(int id, UpdateOrderRequest request);
    bool Delete(int id);
    OrderSummary GetSummary();
}