namespace BI.Sales.Api.Entities.OltpEcommerce;

public class Product
{
    public int ProductId { get; set; }
    public int SubCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int StockQty { get; set; }
    public bool IsActive { get; set; } = true;
    public string? ImageUrl { get; set; }
    public SubCategory? SubCategory { get; set; }
}
