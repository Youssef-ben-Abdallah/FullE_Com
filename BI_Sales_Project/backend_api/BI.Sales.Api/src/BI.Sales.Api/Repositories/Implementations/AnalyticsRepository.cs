using BI.Sales.Api.Data.DataWarehouse;
using BI.Sales.Api.Entities.DataWarehouse;
using BI.Sales.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BI.Sales.Api.Repositories.Implementations;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly DwSalesDbContext _context;

    public AnalyticsRepository(DwSalesDbContext context)
    {
        _context = context;
    }

    public Task<KpiGlobal?> GetKpisAsync() => _context.KpiGlobals.AsNoTracking().FirstOrDefaultAsync();

    public Task<List<SalesByPeriod>> GetSalesByPeriodAsync(string granularity, DateTime? from, DateTime? to)
    {
        var viewName = granularity switch
        {
            "month" => "vw_Sales_By_Month",
            "year" => "vw_Sales_By_Year",
            _ => "vw_Sales_By_Day"
        };

        var query = _context.SalesByPeriods
            .FromSqlRaw($"SELECT PeriodDate, TotalSales FROM {viewName}")
            .AsQueryable();

        if (from.HasValue)
        {
            query = query.Where(s => s.PeriodDate >= from.Value);
        }

        if (to.HasValue)
        {
            query = query.Where(s => s.PeriodDate <= to.Value);
        }

        return query.AsNoTracking().ToListAsync();
    }

    public Task<List<TopEntity>> GetTopProductsAsync(int top, DateTime? from, DateTime? to)
    {
        var query = _context.TopEntities
            .FromSqlRaw("SELECT TOP ({0}) Name, TotalSales FROM vw_TopProducts_BySales ORDER BY TotalSales DESC", top)
            .AsQueryable();

        if (from.HasValue)
        {
            query = query.Where(s => s.Name != null);
        }

        return query.AsNoTracking().ToListAsync();
    }

    public Task<List<TopEntity>> GetTopCustomersAsync(int top, DateTime? from, DateTime? to)
    {
        var query = _context.TopEntities
            .FromSqlRaw("SELECT TOP ({0}) Name, TotalSales FROM vw_TopCustomers_BySales ORDER BY TotalSales DESC", top)
            .AsQueryable();

        if (from.HasValue)
        {
            query = query.Where(s => s.Name != null);
        }

        return query.AsNoTracking().ToListAsync();
    }

    public Task<List<SalesByTerritory>> GetSalesByTerritoryAsync(DateTime? from, DateTime? to)
    {
        var query = _context.SalesByTerritories.AsQueryable();
        if (from.HasValue)
        {
            query = query.Where(s => s.Territory != null);
        }

        return query.AsNoTracking().ToListAsync();
    }

    public Task<List<SalesByShipMethod>> GetSalesByShipMethodAsync(DateTime? from, DateTime? to)
    {
        var query = _context.SalesByShipMethods.AsQueryable();
        if (from.HasValue)
        {
            query = query.Where(s => s.ShipMethod != null);
        }

        return query.AsNoTracking().ToListAsync();
    }
}
