using BI.Sales.Api.Data.OltpEcommerce;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Repositories.Implementations;

public class CartRepository : ICartRepository
{
    private readonly OltpEcommerceDbContext _context;

    public CartRepository(OltpEcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<ShoppingCart> GetOrCreateCartAsync(string userId)
    {
        var cart = await _context.ShoppingCarts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart != null)
        {
            return cart;
        }

        cart = new ShoppingCart
        {
            CartId = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.ShoppingCarts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public Task<CartItem?> GetCartItemAsync(int itemId, string userId)
    {
        return _context.CartItems
            .Include(i => i.Cart)
            .Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.CartItemId == itemId && i.Cart != null && i.Cart.UserId == userId);
    }

    public async Task AddItemAsync(ShoppingCart cart, CartItem item)
    {
        _context.CartItems.Add(item);
        cart.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(CartItem item)
    {
        _context.CartItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemAsync(CartItem item)
    {
        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
