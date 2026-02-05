using BI.Sales.Api.Entities.OltpEcommerce;

namespace BI.Sales.Api.Repositories.Interfaces;

public interface ICartRepository
{
    Task<ShoppingCart> GetOrCreateCartAsync(string userId);
    Task<CartItem?> GetCartItemAsync(int itemId, string userId);
    Task AddItemAsync(ShoppingCart cart, CartItem item);
    Task UpdateItemAsync(CartItem item);
    Task RemoveItemAsync(CartItem item);
    Task SaveChangesAsync();
}
