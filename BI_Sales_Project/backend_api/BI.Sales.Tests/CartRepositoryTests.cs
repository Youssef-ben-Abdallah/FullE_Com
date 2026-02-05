using BI.Sales.Api.Data.OltpEcommerce;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BI.Sales.Tests;

public class CartRepositoryTests
{
    [Fact]
    public async Task GetOrCreateCartCreatesNewCart()
    {
        var options = new DbContextOptionsBuilder<OltpEcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        await using var context = new OltpEcommerceDbContext(options);
        var repo = new CartRepository(context);

        var cart = await repo.GetOrCreateCartAsync("user-1");

        Assert.NotEqual(Guid.Empty, cart.CartId);
        Assert.Equal("user-1", cart.UserId);
    }

    [Fact]
    public async Task AddItemAddsToCart()
    {
        var options = new DbContextOptionsBuilder<OltpEcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        await using var context = new OltpEcommerceDbContext(options);
        context.Products.Add(new Product { ProductId = 1, Name = "Item", Sku = "SKU-1", UnitPrice = 10, StockQty = 5, SubCategoryId = 1 });
        await context.SaveChangesAsync();

        var repo = new CartRepository(context);
        var cart = await repo.GetOrCreateCartAsync("user-1");

        await repo.AddItemAsync(cart, new CartItem { CartId = cart.CartId, ProductId = 1, Quantity = 2, UnitPriceSnapshot = 10 });

        var itemId = await context.CartItems.Select(i => i.CartItemId).FirstAsync();
        var fetched = await repo.GetCartItemAsync(itemId, "user-1");
        Assert.NotNull(fetched);
        Assert.Equal(2, fetched?.Quantity);
    }
}
