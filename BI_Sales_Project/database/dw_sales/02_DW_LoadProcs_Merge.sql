USE AdventureWorksDW_Sales;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimDate
AS
BEGIN
    SET NOCOUNT ON;
    MERGE dbo.DimDate AS target
    USING (
        SELECT DISTINCT
            CONVERT(int, CONVERT(char(8), d.DateValue, 112)) AS DateKey,
            d.DateValue AS FullDate,
            YEAR(d.DateValue) AS Year,
            DATEPART(QUARTER, d.DateValue) AS Quarter,
            MONTH(d.DateValue) AS MonthNumber,
            DATENAME(MONTH, d.DateValue) AS MonthName,
            DAY(d.DateValue) AS DayOfMonth,
            DATENAME(WEEKDAY, d.DateValue) AS DayName,
            CASE WHEN DATEPART(WEEKDAY, d.DateValue) IN (1,7) THEN 1 ELSE 0 END AS IsWeekend
        FROM AdventureWorks2019.dbo.DimDate AS d
    ) AS src
    ON target.DateKey = src.DateKey
    WHEN MATCHED THEN
        UPDATE SET FullDate = src.FullDate,
                   Year = src.Year,
                   Quarter = src.Quarter,
                   MonthNumber = src.MonthNumber,
                   MonthName = src.MonthName,
                   DayOfMonth = src.DayOfMonth,
                   DayName = src.DayName,
                   IsWeekend = src.IsWeekend
    WHEN NOT MATCHED THEN
        INSERT (DateKey, FullDate, Year, Quarter, MonthNumber, MonthName, DayOfMonth, DayName, IsWeekend)
        VALUES (src.DateKey, src.FullDate, src.Year, src.Quarter, src.MonthNumber, src.MonthName, src.DayOfMonth, src.DayName, src.IsWeekend);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimProduct
AS
BEGIN
    SET NOCOUNT ON;
    MERGE dbo.DimProduct AS target
    USING (
        SELECT ProductID, Name, ProductNumber, Color, StandardCost, ListPrice, Size, Weight,
               SellStartDate, SellEndDate, DiscontinuedDate
        FROM AdventureWorks2019.Production.Product
    ) AS src
    ON target.ProductID_BK = src.ProductID
    WHEN MATCHED THEN
        UPDATE SET Name = src.Name,
                   ProductNumber = src.ProductNumber,
                   Color = src.Color,
                   StandardCost = src.StandardCost,
                   ListPrice = src.ListPrice,
                   Size = src.Size,
                   Weight = src.Weight,
                   SellStartDate = src.SellStartDate,
                   SellEndDate = src.SellEndDate,
                   DiscontinuedDate = src.DiscontinuedDate
    WHEN NOT MATCHED THEN
        INSERT (ProductID_BK, Name, ProductNumber, Color, StandardCost, ListPrice, Size, Weight, SellStartDate, SellEndDate, DiscontinuedDate)
        VALUES (src.ProductID, src.Name, src.ProductNumber, src.Color, src.StandardCost, src.ListPrice, src.Size, src.Weight, src.SellStartDate, src.SellEndDate, src.DiscontinuedDate);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimCustomer
AS
BEGIN
    SET NOCOUNT ON;
    MERGE dbo.DimCustomer AS target
    USING (
        SELECT c.CustomerID, c.AccountNumber,
               COALESCE(p.FirstName + ' ' + p.LastName, s.Name, 'Unknown') AS CustomerName,
               p.PersonType, p.FirstName, p.LastName,
               c.TerritoryID
        FROM AdventureWorks2019.Sales.Customer c
        LEFT JOIN AdventureWorks2019.Person.Person p ON c.PersonID = p.BusinessEntityID
        LEFT JOIN AdventureWorks2019.Sales.Store s ON c.StoreID = s.BusinessEntityID
    ) AS src
    ON target.CustomerID_BK = src.CustomerID
    WHEN MATCHED THEN
        UPDATE SET AccountNumber = src.AccountNumber,
                   CustomerName = src.CustomerName,
                   PersonType = src.PersonType,
                   FirstName = src.FirstName,
                   LastName = src.LastName,
                   TerritoryID_BK = src.TerritoryID
    WHEN NOT MATCHED THEN
        INSERT (CustomerID_BK, AccountNumber, CustomerName, PersonType, FirstName, LastName, TerritoryID_BK)
        VALUES (src.CustomerID, src.AccountNumber, src.CustomerName, src.PersonType, src.FirstName, src.LastName, src.TerritoryID);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimTerritory
AS
BEGIN
    SET NOCOUNT ON;
    MERGE dbo.DimTerritory AS target
    USING (
        SELECT TerritoryID, Name, CountryRegionCode, [Group] AS GroupName
        FROM AdventureWorks2019.Sales.SalesTerritory
    ) AS src
    ON target.TerritoryID_BK = src.TerritoryID
    WHEN MATCHED THEN
        UPDATE SET Name = src.Name,
                   CountryRegionCode = src.CountryRegionCode,
                   GroupName = src.GroupName
    WHEN NOT MATCHED THEN
        INSERT (TerritoryID_BK, Name, CountryRegionCode, GroupName)
        VALUES (src.TerritoryID, src.Name, src.CountryRegionCode, src.GroupName);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimSalesPerson
AS
BEGIN
    SET NOCOUNT ON;
    MERGE dbo.DimSalesPerson AS target
    USING (
        SELECT sp.BusinessEntityID, sp.TerritoryID, p.FirstName, p.LastName,
               sp.SalesQuota, sp.Bonus, sp.CommissionPct
        FROM AdventureWorks2019.Sales.SalesPerson sp
        JOIN AdventureWorks2019.Person.Person p ON sp.BusinessEntityID = p.BusinessEntityID
    ) AS src
    ON target.BusinessEntityID_BK = src.BusinessEntityID
    WHEN MATCHED THEN
        UPDATE SET TerritoryID_BK = src.TerritoryID,
                   FirstName = src.FirstName,
                   LastName = src.LastName,
                   SalesQuota = src.SalesQuota,
                   Bonus = src.Bonus,
                   CommissionPct = src.CommissionPct
    WHEN NOT MATCHED THEN
        INSERT (BusinessEntityID_BK, TerritoryID_BK, FirstName, LastName, SalesQuota, Bonus, CommissionPct)
        VALUES (src.BusinessEntityID, src.TerritoryID, src.FirstName, src.LastName, src.SalesQuota, src.Bonus, src.CommissionPct);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimShipMethod
AS
BEGIN
    SET NOCOUNT ON;
    MERGE dbo.DimShipMethod AS target
    USING (
        SELECT ShipMethodID, Name, ShipBase, ShipRate
        FROM AdventureWorks2019.Purchasing.ShipMethod
    ) AS src
    ON target.ShipMethodID_BK = src.ShipMethodID
    WHEN MATCHED THEN
        UPDATE SET Name = src.Name,
                   ShipBase = src.ShipBase,
                   ShipRate = src.ShipRate
    WHEN NOT MATCHED THEN
        INSERT (ShipMethodID_BK, Name, ShipBase, ShipRate)
        VALUES (src.ShipMethodID, src.Name, src.ShipBase, src.ShipRate);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimCreditCard
AS
BEGIN
    SET NOCOUNT ON;
    MERGE dbo.DimCreditCard AS target
    USING (
        SELECT CreditCardID, CardType, ExpMonth, ExpYear
        FROM AdventureWorks2019.Sales.CreditCard
    ) AS src
    ON target.CreditCardID_BK = src.CreditCardID
    WHEN MATCHED THEN
        UPDATE SET CardType = src.CardType,
                   ExpMonth = src.ExpMonth,
                   ExpYear = src.ExpYear
    WHEN NOT MATCHED THEN
        INSERT (CreditCardID_BK, CardType, ExpMonth, ExpYear)
        VALUES (src.CreditCardID, src.CardType, src.ExpMonth, src.ExpYear);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimCurrencyRate
AS
BEGIN
    SET NOCOUNT ON;
    MERGE dbo.DimCurrencyRate AS target
    USING (
        SELECT CurrencyRateID, CurrencyRateDate, FromCurrencyCode, ToCurrencyCode, AverageRate, EndOfDayRate
        FROM AdventureWorks2019.Sales.CurrencyRate
    ) AS src
    ON target.CurrencyRateID_BK = src.CurrencyRateID
    WHEN MATCHED THEN
        UPDATE SET CurrencyRateDate = src.CurrencyRateDate,
                   FromCurrencyCode = src.FromCurrencyCode,
                   ToCurrencyCode = src.ToCurrencyCode,
                   AverageRate = src.AverageRate,
                   EndOfDayRate = src.EndOfDayRate
    WHEN NOT MATCHED THEN
        INSERT (CurrencyRateID_BK, CurrencyRateDate, FromCurrencyCode, ToCurrencyCode, AverageRate, EndOfDayRate)
        VALUES (src.CurrencyRateID, src.CurrencyRateDate, src.FromCurrencyCode, src.ToCurrencyCode, src.AverageRate, src.EndOfDayRate);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_FactSalesOrderLine
AS
BEGIN
    SET NOCOUNT ON;
    MERGE dbo.FactSalesOrderLine AS target
    USING (
        SELECT sod.SalesOrderID,
               sod.SalesOrderDetailID,
               soh.SalesOrderNumber,
               dp.ProductKey,
               dc.CustomerKey,
               dt.TerritoryKey,
               dsp.SalesPersonKey,
               dsm.ShipMethodKey,
               dcc.CreditCardKey,
               dcr.CurrencyRateKey,
               CONVERT(int, CONVERT(char(8), soh.OrderDate, 112)) AS OrderDateKey,
               CONVERT(int, CONVERT(char(8), soh.DueDate, 112)) AS DueDateKey,
               CONVERT(int, CONVERT(char(8), soh.ShipDate, 112)) AS ShipDateKey,
               sod.OrderQty,
               sod.UnitPrice,
               sod.UnitPriceDiscount,
               sod.LineTotal
        FROM AdventureWorks2019.Sales.SalesOrderDetail sod
        JOIN AdventureWorks2019.Sales.SalesOrderHeader soh ON sod.SalesOrderID = soh.SalesOrderID
        JOIN dbo.DimProduct dp ON dp.ProductID_BK = sod.ProductID
        JOIN dbo.DimCustomer dc ON dc.CustomerID_BK = soh.CustomerID
        JOIN dbo.DimTerritory dt ON dt.TerritoryID_BK = soh.TerritoryID
        LEFT JOIN dbo.DimSalesPerson dsp ON dsp.BusinessEntityID_BK = soh.SalesPersonID
        JOIN dbo.DimShipMethod dsm ON dsm.ShipMethodID_BK = soh.ShipMethodID
        LEFT JOIN dbo.DimCreditCard dcc ON dcc.CreditCardID_BK = soh.CreditCardID
        LEFT JOIN dbo.DimCurrencyRate dcr ON dcr.CurrencyRateID_BK = soh.CurrencyRateID
    ) AS src
    ON target.SalesOrderDetailID_Degenerate = src.SalesOrderDetailID
    WHEN MATCHED THEN
        UPDATE SET UnitPrice = src.UnitPrice,
                   UnitPriceDiscount = src.UnitPriceDiscount,
                   LineTotal = src.LineTotal
    WHEN NOT MATCHED THEN
        INSERT (SalesOrderID_Degenerate, SalesOrderDetailID_Degenerate, SalesOrderNumber_Degenerate, ProductKey, CustomerKey, TerritoryKey, SalesPersonKey, ShipMethodKey, CreditCardKey, CurrencyRateKey, OrderDateKey, DueDateKey, ShipDateKey, OrderQty, UnitPrice, UnitPriceDiscount, LineTotal)
        VALUES (src.SalesOrderID, src.SalesOrderDetailID, src.SalesOrderNumber, src.ProductKey, src.CustomerKey, src.TerritoryKey, src.SalesPersonKey, src.ShipMethodKey, src.CreditCardKey, src.CurrencyRateKey, src.OrderDateKey, src.DueDateKey, src.ShipDateKey, src.OrderQty, src.UnitPrice, src.UnitPriceDiscount, src.LineTotal);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END;
GO
