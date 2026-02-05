USE AdventureWorksDW_Sales;
GO

-- Source counts
SELECT COUNT(*) AS SourceRows
FROM AdventureWorks2019.Sales.SalesOrderDetail;

-- DW counts
SELECT COUNT(*) AS DwRows
FROM dbo.FactSalesOrderLine;

-- Source totals
SELECT SUM(LineTotal) AS SourceTotal
FROM AdventureWorks2019.Sales.SalesOrderDetail;

-- DW totals
SELECT SUM(LineTotal) AS DwTotal
FROM dbo.FactSalesOrderLine;
