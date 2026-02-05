namespace BI.Sales.Api.Entities.OltpEcommerce;

public class CartItem
{
    public int CartItemId { get; set; }
    public Guid CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPriceSnapshot { get; set; }
    public ShoppingCart? Cart { get; set; }
    public Product? Product { get; set; }
}
