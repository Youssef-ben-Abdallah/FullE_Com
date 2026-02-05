using BI.Sales.Api.Data.OltpEcommerce;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Repositories.Implementations;

public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly OltpEcommerceDbContext _context;

    public SubCategoryRepository(OltpEcommerceDbContext context)
    {
        _context = context;
    }

    public Task<List<SubCategory>> GetByCategoryIdAsync(int categoryId) =>
        _context.SubCategories.Where(s => s.CategoryId == categoryId).AsNoTracking().ToListAsync();

    public Task<SubCategory?> GetByIdAsync(int id) => _context.SubCategories.FindAsync(id).AsTask();

    public async Task<SubCategory> AddAsync(SubCategory subCategory)
    {
        _context.SubCategories.Add(subCategory);
        await _context.SaveChangesAsync();
        return subCategory;
    }

    public async Task UpdateAsync(SubCategory subCategory)
    {
        _context.SubCategories.Update(subCategory);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SubCategory subCategory)
    {
        _context.SubCategories.Remove(subCategory);
        await _context.SaveChangesAsync();
    }
}
