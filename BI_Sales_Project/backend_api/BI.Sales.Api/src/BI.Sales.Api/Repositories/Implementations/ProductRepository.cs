using BI.Sales.Api.Data.OltpEcommerce;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly OltpEcommerceDbContext _context;

    public ProductRepository(OltpEcommerceDbContext context)
    {
        _context = context;
    }

    public Task<List<Product>> GetAllAsync(int? categoryId, int? subCategoryId, string? q, decimal? minPrice, decimal? maxPrice, bool? active)
    {
        var query = _context.Products.AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.SubCategory != null && p.SubCategory.CategoryId == categoryId.Value);
        }

        if (subCategoryId.HasValue)
        {
            query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(p => p.Name.Contains(q) || p.Sku.Contains(q));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.UnitPrice >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.UnitPrice <= maxPrice.Value);
        }

        if (active.HasValue)
        {
            query = query.Where(p => p.IsActive == active.Value);
        }

        return query.AsNoTracking().ToListAsync();
    }

    public Task<Product?> GetByIdAsync(int id) => _context.Products.FindAsync(id).AsTask();

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
