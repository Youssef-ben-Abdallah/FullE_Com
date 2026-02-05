using BI.Sales.Api.Data.OltpEcommerce;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly OltpEcommerceDbContext _context;

    public CategoryRepository(OltpEcommerceDbContext context)
    {
        _context = context;
    }

    public Task<List<Category>> GetAllAsync() => _context.Categories.AsNoTracking().ToListAsync();

    public Task<Category?> GetByIdAsync(int id) => _context.Categories.FindAsync(id).AsTask();

    public async Task<Category> AddAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}
