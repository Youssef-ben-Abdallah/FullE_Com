USE AdventureWorksDW_Sales;
GO

CREATE OR ALTER VIEW dbo.vw_KPIs_Global AS
SELECT
    SUM(LineTotal) AS TotalSales,
    COUNT(DISTINCT SalesOrderID_Degenerate) AS TotalOrders,
    COUNT(DISTINCT CustomerKey) AS TotalCustomers,
    AVG(LineTotal) AS AvgOrderValue,
    AVG(UnitPriceDiscount) AS AvgDiscount
FROM dbo.FactSalesOrderLine;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_Day AS
SELECT OrderDateKey AS DateKey, SUM(LineTotal) AS TotalSales
FROM dbo.FactSalesOrderLine
GROUP BY OrderDateKey;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_Month AS
SELECT (OrderDateKey / 100) AS YearMonth, SUM(LineTotal) AS TotalSales
FROM dbo.FactSalesOrderLine
GROUP BY (OrderDateKey / 100);
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_Year AS
SELECT (OrderDateKey / 10000) AS [Year], SUM(LineTotal) AS TotalSales
FROM dbo.FactSalesOrderLine
GROUP BY (OrderDateKey / 10000);
GO

CREATE OR ALTER VIEW dbo.vw_TopProducts_BySales AS
SELECT TOP 50 dp.Name, SUM(f.LineTotal) AS TotalSales
FROM dbo.FactSalesOrderLine f
JOIN dbo.DimProduct dp ON f.ProductKey = dp.ProductKey
GROUP BY dp.Name
ORDER BY TotalSales DESC;
GO

CREATE OR ALTER VIEW dbo.vw_TopCustomers_BySales AS
SELECT TOP 50 dc.CustomerName, SUM(f.LineTotal) AS TotalSales
FROM dbo.FactSalesOrderLine f
JOIN dbo.DimCustomer dc ON f.CustomerKey = dc.CustomerKey
GROUP BY dc.CustomerName
ORDER BY TotalSales DESC;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_Territory AS
SELECT dt.Name AS Territory, SUM(f.LineTotal) AS TotalSales
FROM dbo.FactSalesOrderLine f
JOIN dbo.DimTerritory dt ON f.TerritoryKey = dt.TerritoryKey
GROUP BY dt.Name;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_By_ShipMethod AS
SELECT dsm.Name AS ShipMethod, SUM(f.LineTotal) AS TotalSales
FROM dbo.FactSalesOrderLine f
JOIN dbo.DimShipMethod dsm ON f.ShipMethodKey = dsm.ShipMethodKey
GROUP BY dsm.Name;
GO

CREATE OR ALTER VIEW dbo.vw_Sales_Trends_MA7_MA30 AS
SELECT
    d.FullDate,
    SUM(f.LineTotal) AS TotalSales
FROM dbo.FactSalesOrderLine f
JOIN dbo.DimDate d ON f.OrderDateKey = d.DateKey
GROUP BY d.FullDate;
GO
