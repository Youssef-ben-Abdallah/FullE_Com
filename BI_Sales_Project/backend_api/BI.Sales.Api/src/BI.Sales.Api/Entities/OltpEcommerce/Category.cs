namespace BI.Sales.Api.Entities.OltpEcommerce;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
