using BI.Sales.Api.DTOs;
using BI.Sales.Api.Entities.OltpEcommerce;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/subcategories")]
public class SubCategoriesController : ControllerBase
{
    private readonly ISubCategoryRepository _subCategoryRepository;

    public SubCategoriesController(ISubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<SubCategoryDto>> Create(SubCategoryDto request)
    {
        var subCategory = new SubCategory
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            Description = request.Description
        };

        subCategory = await _subCategoryRepository.AddAsync(subCategory);
        return Ok(new SubCategoryDto(subCategory.SubCategoryId, subCategory.CategoryId, subCategory.Name, subCategory.Description));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, SubCategoryDto request)
    {
        var subCategory = await _subCategoryRepository.GetByIdAsync(id);
        if (subCategory == null)
        {
            return NotFound();
        }

        subCategory.CategoryId = request.CategoryId;
        subCategory.Name = request.Name;
        subCategory.Description = request.Description;
        await _subCategoryRepository.UpdateAsync(subCategory);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var subCategory = await _subCategoryRepository.GetByIdAsync(id);
        if (subCategory == null)
        {
            return NotFound();
        }

        await _subCategoryRepository.DeleteAsync(subCategory);
        return NoContent();
    }
}
