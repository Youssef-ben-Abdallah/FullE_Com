using BI.Sales.Api.Security;

namespace BI.Sales.Api.Entities.OltpEcommerce;

public class ShoppingCart
{
    public Guid CartId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ApplicationUser? User { get; set; }
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}
