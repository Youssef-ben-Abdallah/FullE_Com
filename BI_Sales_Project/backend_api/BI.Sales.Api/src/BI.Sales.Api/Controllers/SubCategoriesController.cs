using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/subcategories")]
public class SubCategoriesController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateSubCategory() => Ok();

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public IActionResult UpdateSubCategory(int id) => Ok();

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public IActionResult DeleteSubCategory(int id) => Ok();
}
