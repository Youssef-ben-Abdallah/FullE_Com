using BI.Sales.Api.Entities.DataWarehouse;

namespace BI.Sales.Api.Repositories.Interfaces;

public interface IAnalyticsRepository
{
    Task<KpiGlobal?> GetKpisAsync();
    Task<List<SalesByPeriod>> GetSalesByPeriodAsync(string granularity, DateTime? from, DateTime? to);
    Task<List<TopEntity>> GetTopProductsAsync(int top, DateTime? from, DateTime? to);
    Task<List<TopEntity>> GetTopCustomersAsync(int top, DateTime? from, DateTime? to);
    Task<List<SalesByTerritory>> GetSalesByTerritoryAsync(DateTime? from, DateTime? to);
    Task<List<SalesByShipMethod>> GetSalesByShipMethodAsync(DateTime? from, DateTime? to);
}
