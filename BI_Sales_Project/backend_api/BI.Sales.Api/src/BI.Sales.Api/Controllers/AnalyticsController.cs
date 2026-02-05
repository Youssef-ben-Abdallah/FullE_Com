using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    [Authorize]
    [HttpGet("kpis")]
    public IActionResult GetKpis() => Ok();

    [Authorize]
    [HttpGet("sales/time")]
    public IActionResult GetSalesOverTime() => Ok();

    [Authorize]
    [HttpGet("top/products")]
    public IActionResult GetTopProducts() => Ok();

    [Authorize]
    [HttpGet("top/customers")]
    public IActionResult GetTopCustomers() => Ok();

    [Authorize]
    [HttpGet("by/territory")]
    public IActionResult GetByTerritory() => Ok();

    [Authorize]
    [HttpGet("by/shipmethod")]
    public IActionResult GetByShipMethod() => Ok();
}
