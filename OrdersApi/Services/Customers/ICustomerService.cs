using OrdersApi.Models;

namespace OrdersApi.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> GetById(int id);
    Task<IEnumerable<Order>> GetOrdersByCustomer(int id);
    Task<Customer> Create(CreateCustomerRequest request);
    Task<bool> Delete(int id);
}