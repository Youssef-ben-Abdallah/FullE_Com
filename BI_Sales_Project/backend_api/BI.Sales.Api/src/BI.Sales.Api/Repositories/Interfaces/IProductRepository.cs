using BI.Sales.Api.Entities.OltpEcommerce;

namespace BI.Sales.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(int? categoryId, int? subCategoryId, string? q, decimal? minPrice, decimal? maxPrice, bool? active);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}
