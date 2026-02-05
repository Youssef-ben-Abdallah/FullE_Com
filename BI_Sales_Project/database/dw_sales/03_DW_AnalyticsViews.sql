USE AdventureWorksDW_Sales;
GO

CREATE OR ALTER VIEW dbo.vw_KPIs_Global
AS
SELECT
    TotalSales = SUM(LineTotal),
    TotalOrders = COUNT(DISTINCT SalesOrderID_Degenerate),
    TotalCustomers = COUNT(DISTINCT CustomerKey),
    AvgOrderValue = AVG(LineTotal),
    AvgDiscount = AVG(UnitPriceDiscount)
FROM dbo.FactSalesOrderLine;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_Day
AS
SELECT
    PeriodDate = d.FullDate,
    TotalSales = SUM(f.LineTotal)
FROM dbo.FactSalesOrderLine f
INNER JOIN dbo.DimDate d ON f.OrderDateKey = d.DateKey
GROUP BY d.FullDate;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_Month
AS
SELECT
    PeriodDate = DATEFROMPARTS(d.Year, d.MonthNumber, 1),
    TotalSales = SUM(f.LineTotal)
FROM dbo.FactSalesOrderLine f
INNER JOIN dbo.DimDate d ON f.OrderDateKey = d.DateKey
GROUP BY d.Year, d.MonthNumber;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_Year
AS
SELECT
    PeriodDate = DATEFROMPARTS(d.Year, 1, 1),
    TotalSales = SUM(f.LineTotal)
FROM dbo.FactSalesOrderLine f
INNER JOIN dbo.DimDate d ON f.OrderDateKey = d.DateKey
GROUP BY d.Year;
GO

CREATE OR ALTER VIEW dbo.vw_TopProducts_BySales
AS
SELECT TOP 100
    Name = p.Name,
    TotalSales = SUM(f.LineTotal)
FROM dbo.FactSalesOrderLine f
INNER JOIN dbo.DimProduct p ON f.ProductKey = p.ProductKey
GROUP BY p.Name
ORDER BY TotalSales DESC;
GO

CREATE OR ALTER VIEW dbo.vw_TopCustomers_BySales
AS
SELECT TOP 100
    Name = c.CustomerName,
    TotalSales = SUM(f.LineTotal)
FROM dbo.FactSalesOrderLine f
INNER JOIN dbo.DimCustomer c ON f.CustomerKey = c.CustomerKey
GROUP BY c.CustomerName
ORDER BY TotalSales DESC;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_Territory
AS
SELECT
    Territory = t.Name,
    TotalSales = SUM(f.LineTotal)
FROM dbo.FactSalesOrderLine f
INNER JOIN dbo.DimTerritory t ON f.TerritoryKey = t.TerritoryKey
GROUP BY t.Name;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_ShipMethod
AS
SELECT
    ShipMethod = s.Name,
    TotalSales = SUM(f.LineTotal)
FROM dbo.FactSalesOrderLine f
INNER JOIN dbo.DimShipMethod s ON f.ShipMethodKey = s.ShipMethodKey
GROUP BY s.Name;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_Trends_MA7_MA30
AS
SELECT
    d.FullDate,
    Sales = SUM(f.LineTotal),
    MA7 = AVG(SUM(f.LineTotal)) OVER (ORDER BY d.FullDate ROWS BETWEEN 6 PRECEDING AND CURRENT ROW),
    MA30 = AVG(SUM(f.LineTotal)) OVER (ORDER BY d.FullDate ROWS BETWEEN 29 PRECEDING AND CURRENT ROW)
FROM dbo.FactSalesOrderLine f
INNER JOIN dbo.DimDate d ON f.OrderDateKey = d.DateKey
GROUP BY d.FullDate;
GO
