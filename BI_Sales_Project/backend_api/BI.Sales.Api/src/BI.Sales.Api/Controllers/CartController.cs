using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetCart() => Ok();

    [Authorize]
    [HttpPost("items")]
    public IActionResult AddItem() => Ok();

    [Authorize]
    [HttpPut("items/{itemId:int}")]
    public IActionResult UpdateItem(int itemId) => Ok();

    [Authorize]
    [HttpDelete("items/{itemId:int}")]
    public IActionResult RemoveItem(int itemId) => Ok();

    [Authorize]
    [HttpPost("checkout")]
    public IActionResult Checkout() => Ok();
}
