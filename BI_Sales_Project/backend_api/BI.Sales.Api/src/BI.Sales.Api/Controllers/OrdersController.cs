using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    [Authorize]
    [HttpGet("my")]
    public IActionResult GetMyOrders() => Ok();

    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetOrder(int id) => Ok();
}

[ApiController]
[Route("api/admin/orders")]
public class AdminOrdersController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult GetAllOrders() => Ok();

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}/status")]
    public IActionResult UpdateStatus(int id) => Ok();
}
