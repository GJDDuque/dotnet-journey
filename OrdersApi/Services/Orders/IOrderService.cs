using OrdersApi.Models;

namespace OrdersApi.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAll(string? status);
    Task<Order?> GetById(int id);
    Task<Order> Create(CreateOrderRequest request);
    Task<Order?> Update(int id, UpdateOrderRequest request);
    Task<bool> Delete(int id);
    Task<OrderSummary> GetSummary();
}