namespace BI.Sales.Api.DTOs;

public record KpiDto(decimal TotalSales, int TotalOrders, int TotalCustomers, decimal AvgOrderValue, decimal AvgDiscount);
public record SalesByPeriodDto(DateTime PeriodDate, decimal TotalSales);
public record TopEntityDto(string Name, decimal TotalSales);
public record SalesByTerritoryDto(string Territory, decimal TotalSales);
public record SalesByShipMethodDto(string ShipMethod, decimal TotalSales);
