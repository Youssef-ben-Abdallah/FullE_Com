USE AdventureWorksDW_Sales;
GO

SELECT COUNT(*) AS SourceCount, SUM(LineTotal) AS SourceTotal
FROM AdventureWorks2019.Sales.SalesOrderDetail;

SELECT COUNT(*) AS DwCount, SUM(LineTotal) AS DwTotal
FROM dbo.FactSalesOrderLine;
