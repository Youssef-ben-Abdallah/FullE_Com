using BI.Sales.Api.Entities.OltpEcommerce;

namespace BI.Sales.Api.Repositories.Interfaces;

public interface ISubCategoryRepository
{
    Task<List<SubCategory>> GetByCategoryIdAsync(int categoryId);
    Task<SubCategory?> GetByIdAsync(int id);
    Task<SubCategory> AddAsync(SubCategory subCategory);
    Task UpdateAsync(SubCategory subCategory);
    Task DeleteAsync(SubCategory subCategory);
}
