namespace BI.Sales.Api.Entities.OltpEcommerce;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPriceSnapshot { get; set; }
    public decimal LineTotal { get; set; }
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
