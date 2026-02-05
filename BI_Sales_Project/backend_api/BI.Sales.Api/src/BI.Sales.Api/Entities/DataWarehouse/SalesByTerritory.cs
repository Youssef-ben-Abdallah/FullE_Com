namespace BI.Sales.Api.Entities.DataWarehouse;

public class SalesByTerritory
{
    public string Territory { get; set; } = string.Empty;
    public decimal TotalSales { get; set; }
}
