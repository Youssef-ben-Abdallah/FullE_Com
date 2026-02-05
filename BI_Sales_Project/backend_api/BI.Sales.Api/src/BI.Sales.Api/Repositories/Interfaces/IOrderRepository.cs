using BI.Sales.Api.Entities.OltpEcommerce;

namespace BI.Sales.Api.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersForUserAsync(string userId);
    Task<Order?> GetOrderAsync(int id, string userId, bool isAdmin);
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> AddOrderAsync(Order order);
    Task UpdateAsync(Order order);
    Task SaveChangesAsync();
}
