namespace BI.Sales.Api.DTOs;

public record CategoryDto(int CategoryId, string Name, string? Description);
public record SubCategoryDto(int SubCategoryId, int CategoryId, string Name, string? Description);
public record ProductDto(int ProductId, int SubCategoryId, string Name, string Sku, decimal UnitPrice, int StockQty, bool IsActive, string? ImageUrl);
public record ProductFilterDto(int? CategoryId, int? SubCategoryId, string? Q, decimal? MinPrice, decimal? MaxPrice, bool? Active);
