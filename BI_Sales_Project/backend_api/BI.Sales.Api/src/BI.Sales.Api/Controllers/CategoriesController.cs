using BI.Sales.Api.DTOs;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository)
    {
        _categoryRepository = categoryRepository;
        _subCategoryRepository = subCategoryRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return Ok(categories.Select(c => new CategoryDto(c.CategoryId, c.Name, c.Description)));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CategoryDto>> Create(CategoryDto request)
    {
        var category = new Category { Name = request.Name, Description = request.Description };
        category = await _categoryRepository.AddAsync(category);
        return Ok(new CategoryDto(category.CategoryId, category.Name, category.Description));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, CategoryDto request)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        category.Name = request.Name;
        category.Description = request.Description;
        await _categoryRepository.UpdateAsync(category);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        await _categoryRepository.DeleteAsync(category);
        return NoContent();
    }

    [HttpGet("{id:int}/subcategories")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<SubCategoryDto>>> GetSubCategories(int id)
    {
        var subCategories = await _subCategoryRepository.GetByCategoryIdAsync(id);
        return Ok(subCategories.Select(s => new SubCategoryDto(s.SubCategoryId, s.CategoryId, s.Name, s.Description)));
    }
}
