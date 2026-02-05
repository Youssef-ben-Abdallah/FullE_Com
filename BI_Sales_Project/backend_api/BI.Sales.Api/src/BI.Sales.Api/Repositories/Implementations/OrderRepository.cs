using BI.Sales.Api.Data.OltpEcommerce;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly OltpEcommerceDbContext _context;

    public OrderRepository(OltpEcommerceDbContext context)
    {
        _context = context;
    }

    public Task<List<Order>> GetOrdersForUserAsync(string userId) =>
        _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

    public Task<Order?> GetOrderAsync(int id, string userId, bool isAdmin)
    {
        var query = _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .AsQueryable();

        if (!isAdmin)
        {
            query = query.Where(o => o.UserId == userId);
        }

        return query.FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public Task<List<Order>> GetAllOrdersAsync() =>
        _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .OrderByDescending(o => o.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Order> AddOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
