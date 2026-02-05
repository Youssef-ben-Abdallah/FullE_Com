using BI.Sales.Api.DTOs;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BI.Sales.Api.Controllers;

[ApiController]
[Route("api/analytics")]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsRepository _analyticsRepository;

    public AnalyticsController(IAnalyticsRepository analyticsRepository)
    {
        _analyticsRepository = analyticsRepository;
    }

    [HttpGet("kpis")]
    public async Task<ActionResult<KpiDto>> GetKpis()
    {
        var kpis = await _analyticsRepository.GetKpisAsync();
        if (kpis == null)
        {
            return NotFound();
        }

        return Ok(new KpiDto(kpis.TotalSales, kpis.TotalOrders, kpis.TotalCustomers, kpis.AvgOrderValue, kpis.AvgDiscount));
    }

    [HttpGet("sales/time")]
    public async Task<ActionResult<IEnumerable<SalesByPeriodDto>>> GetSalesByTime([FromQuery] string granularity = "day", [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
    {
        var data = await _analyticsRepository.GetSalesByPeriodAsync(granularity, from, to);
        return Ok(data.Select(d => new SalesByPeriodDto(d.PeriodDate, d.TotalSales)));
    }

    [HttpGet("top/products")]
    public async Task<ActionResult<IEnumerable<TopEntityDto>>> GetTopProducts([FromQuery] int top = 10, [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
    {
        var data = await _analyticsRepository.GetTopProductsAsync(top, from, to);
        return Ok(data.Select(d => new TopEntityDto(d.Name, d.TotalSales)));
    }

    [HttpGet("top/customers")]
    public async Task<ActionResult<IEnumerable<TopEntityDto>>> GetTopCustomers([FromQuery] int top = 10, [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
    {
        var data = await _analyticsRepository.GetTopCustomersAsync(top, from, to);
        return Ok(data.Select(d => new TopEntityDto(d.Name, d.TotalSales)));
    }

    [HttpGet("by/territory")]
    public async Task<ActionResult<IEnumerable<SalesByTerritoryDto>>> GetByTerritory([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
    {
        var data = await _analyticsRepository.GetSalesByTerritoryAsync(from, to);
        return Ok(data.Select(d => new SalesByTerritoryDto(d.Territory, d.TotalSales)));
    }

    [HttpGet("by/shipmethod")]
    public async Task<ActionResult<IEnumerable<SalesByShipMethodDto>>> GetByShipMethod([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
    {
        var data = await _analyticsRepository.GetSalesByShipMethodAsync(from, to);
        return Ok(data.Select(d => new SalesByShipMethodDto(d.ShipMethod, d.TotalSales)));
    }
}
