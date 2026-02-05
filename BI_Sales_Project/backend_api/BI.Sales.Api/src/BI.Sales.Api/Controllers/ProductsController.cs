using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetProducts() => Ok();

    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetProduct(int id) => Ok();

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateProduct() => Ok();

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public IActionResult UpdateProduct(int id) => Ok();

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public IActionResult DeleteProduct(int id) => Ok();
}
