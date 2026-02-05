using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetCategories() => Ok();

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateCategory() => Ok();

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public IActionResult UpdateCategory(int id) => Ok();

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public IActionResult DeleteCategory(int id) => Ok();

    [Authorize]
    [HttpGet("{id:int}/subcategories")]
    public IActionResult GetSubCategories(int id) => Ok();
}
