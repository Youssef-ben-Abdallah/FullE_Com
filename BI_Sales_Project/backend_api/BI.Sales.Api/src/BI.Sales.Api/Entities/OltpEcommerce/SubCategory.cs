namespace BI.Sales.Api.Entities.OltpEcommerce;

public class SubCategory
{
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Category? Category { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
