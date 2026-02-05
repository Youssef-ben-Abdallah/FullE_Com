namespace BI.Sales.Api.Entities.DataWarehouse;

public class KpiGlobal
{
    public decimal TotalSales { get; set; }
    public int TotalOrders { get; set; }
    public int TotalCustomers { get; set; }
    public decimal AvgOrderValue { get; set; }
    public decimal AvgDiscount { get; set; }
}
