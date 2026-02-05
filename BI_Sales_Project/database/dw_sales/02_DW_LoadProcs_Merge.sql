USE AdventureWorksDW_Sales;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimDate
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.DimDate AS target
    USING (
        SELECT DISTINCT
            DateKey = CAST(CONVERT(VARCHAR(8), OrderDate, 112) AS INT),
            FullDate = CAST(OrderDate AS DATE),
            Year = YEAR(OrderDate),
            Quarter = DATEPART(QUARTER, OrderDate),
            MonthNumber = MONTH(OrderDate),
            MonthName = DATENAME(MONTH, OrderDate),
            DayOfMonth = DAY(OrderDate),
            DayName = DATENAME(WEEKDAY, OrderDate),
            IsWeekend = CASE WHEN DATENAME(WEEKDAY, OrderDate) IN ('Saturday','Sunday') THEN 1 ELSE 0 END
        FROM AdventureWorks2019.Sales.SalesOrderHeader
    ) AS source
    ON target.DateKey = source.DateKey
    WHEN MATCHED THEN
        UPDATE SET
            FullDate = source.FullDate,
            Year = source.Year,
            Quarter = source.Quarter,
            MonthNumber = source.MonthNumber,
            MonthName = source.MonthName,
            DayOfMonth = source.DayOfMonth,
            DayName = source.DayName,
            IsWeekend = source.IsWeekend
    WHEN NOT MATCHED THEN
        INSERT (DateKey, FullDate, Year, Quarter, MonthNumber, MonthName, DayOfMonth, DayName, IsWeekend)
        VALUES (source.DateKey, source.FullDate, source.Year, source.Quarter, source.MonthNumber, source.MonthName, source.DayOfMonth, source.DayName, source.IsWeekend);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimProduct
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.DimProduct AS target
    USING (
        SELECT ProductID_BK = ProductID,
               Name,
               ProductNumber,
               Color,
               StandardCost,
               ListPrice,
               Size,
               Weight,
               SellStartDate,
               SellEndDate,
               DiscontinuedDate
        FROM AdventureWorks2019.Production.Product
    ) AS source
    ON target.ProductID_BK = source.ProductID_BK
    WHEN MATCHED THEN
        UPDATE SET
            Name = source.Name,
            ProductNumber = source.ProductNumber,
            Color = source.Color,
            StandardCost = source.StandardCost,
            ListPrice = source.ListPrice,
            Size = source.Size,
            Weight = source.Weight,
            SellStartDate = source.SellStartDate,
            SellEndDate = source.SellEndDate,
            DiscontinuedDate = source.DiscontinuedDate
    WHEN NOT MATCHED THEN
        INSERT (ProductID_BK, Name, ProductNumber, Color, StandardCost, ListPrice, Size, Weight, SellStartDate, SellEndDate, DiscontinuedDate)
        VALUES (source.ProductID_BK, source.Name, source.ProductNumber, source.Color, source.StandardCost, source.ListPrice, source.Size, source.Weight, source.SellStartDate, source.SellEndDate, source.DiscontinuedDate);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimCustomer
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.DimCustomer AS target
    USING (
        SELECT c.CustomerID AS CustomerID_BK,
               c.AccountNumber,
               CustomerName = COALESCE(p.FirstName + ' ' + p.LastName, s.Name, 'Unknown'),
               p.PersonType,
               p.FirstName,
               p.LastName,
               c.TerritoryID AS TerritoryID_BK
        FROM AdventureWorks2019.Sales.Customer c
        LEFT JOIN AdventureWorks2019.Person.Person p ON c.PersonID = p.BusinessEntityID
        LEFT JOIN AdventureWorks2019.Sales.Store s ON c.StoreID = s.BusinessEntityID
    ) AS source
    ON target.CustomerID_BK = source.CustomerID_BK
    WHEN MATCHED THEN
        UPDATE SET
            AccountNumber = source.AccountNumber,
            CustomerName = source.CustomerName,
            PersonType = source.PersonType,
            FirstName = source.FirstName,
            LastName = source.LastName,
            TerritoryID_BK = source.TerritoryID_BK
    WHEN NOT MATCHED THEN
        INSERT (CustomerID_BK, AccountNumber, CustomerName, PersonType, FirstName, LastName, TerritoryID_BK)
        VALUES (source.CustomerID_BK, source.AccountNumber, source.CustomerName, source.PersonType, source.FirstName, source.LastName, source.TerritoryID_BK);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimTerritory
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.DimTerritory AS target
    USING (
        SELECT TerritoryID AS TerritoryID_BK, Name, CountryRegionCode, [Group] AS GroupName
        FROM AdventureWorks2019.Sales.SalesTerritory
    ) AS source
    ON target.TerritoryID_BK = source.TerritoryID_BK
    WHEN MATCHED THEN
        UPDATE SET Name = source.Name, CountryRegionCode = source.CountryRegionCode, GroupName = source.GroupName
    WHEN NOT MATCHED THEN
        INSERT (TerritoryID_BK, Name, CountryRegionCode, GroupName)
        VALUES (source.TerritoryID_BK, source.Name, source.CountryRegionCode, source.GroupName);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimSalesPerson
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.DimSalesPerson AS target
    USING (
        SELECT sp.BusinessEntityID AS BusinessEntityID_BK,
               sp.TerritoryID AS TerritoryID_BK,
               p.FirstName,
               p.LastName,
               sp.SalesQuota,
               sp.Bonus,
               sp.CommissionPct
        FROM AdventureWorks2019.Sales.SalesPerson sp
        INNER JOIN AdventureWorks2019.Person.Person p ON sp.BusinessEntityID = p.BusinessEntityID
    ) AS source
    ON target.BusinessEntityID_BK = source.BusinessEntityID_BK
    WHEN MATCHED THEN
        UPDATE SET TerritoryID_BK = source.TerritoryID_BK, FirstName = source.FirstName, LastName = source.LastName,
                   SalesQuota = source.SalesQuota, Bonus = source.Bonus, CommissionPct = source.CommissionPct
    WHEN NOT MATCHED THEN
        INSERT (BusinessEntityID_BK, TerritoryID_BK, FirstName, LastName, SalesQuota, Bonus, CommissionPct)
        VALUES (source.BusinessEntityID_BK, source.TerritoryID_BK, source.FirstName, source.LastName, source.SalesQuota, source.Bonus, source.CommissionPct);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimShipMethod
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.DimShipMethod AS target
    USING (
        SELECT ShipMethodID AS ShipMethodID_BK, Name, ShipBase, ShipRate
        FROM AdventureWorks2019.Purchasing.ShipMethod
    ) AS source
    ON target.ShipMethodID_BK = source.ShipMethodID_BK
    WHEN MATCHED THEN
        UPDATE SET Name = source.Name, ShipBase = source.ShipBase, ShipRate = source.ShipRate
    WHEN NOT MATCHED THEN
        INSERT (ShipMethodID_BK, Name, ShipBase, ShipRate)
        VALUES (source.ShipMethodID_BK, source.Name, source.ShipBase, source.ShipRate);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimCreditCard
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.DimCreditCard AS target
    USING (
        SELECT CreditCardID AS CreditCardID_BK, CardType, ExpMonth, ExpYear
        FROM AdventureWorks2019.Sales.CreditCard
    ) AS source
    ON target.CreditCardID_BK = source.CreditCardID_BK
    WHEN MATCHED THEN
        UPDATE SET CardType = source.CardType, ExpMonth = source.ExpMonth, ExpYear = source.ExpYear
    WHEN NOT MATCHED THEN
        INSERT (CreditCardID_BK, CardType, ExpMonth, ExpYear)
        VALUES (source.CreditCardID_BK, source.CardType, source.ExpMonth, source.ExpYear);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_DimCurrencyRate
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.DimCurrencyRate AS target
    USING (
        SELECT CurrencyRateID AS CurrencyRateID_BK, CurrencyRateDate, FromCurrencyCode, ToCurrencyCode, AverageRate, EndOfDayRate
        FROM AdventureWorks2019.Sales.CurrencyRate
    ) AS source
    ON target.CurrencyRateID_BK = source.CurrencyRateID_BK
    WHEN MATCHED THEN
        UPDATE SET CurrencyRateDate = source.CurrencyRateDate, FromCurrencyCode = source.FromCurrencyCode,
                   ToCurrencyCode = source.ToCurrencyCode, AverageRate = source.AverageRate, EndOfDayRate = source.EndOfDayRate
    WHEN NOT MATCHED THEN
        INSERT (CurrencyRateID_BK, CurrencyRateDate, FromCurrencyCode, ToCurrencyCode, AverageRate, EndOfDayRate)
        VALUES (source.CurrencyRateID_BK, source.CurrencyRateDate, source.FromCurrencyCode, source.ToCurrencyCode, source.AverageRate, source.EndOfDayRate);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_Load_FactSalesOrderLine
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.FactSalesOrderLine AS target
    USING (
        SELECT
            soh.SalesOrderID AS SalesOrderID_Degenerate,
            sod.SalesOrderDetailID AS SalesOrderDetailID_Degenerate,
            soh.SalesOrderNumber AS SalesOrderNumber_Degenerate,
            dp.ProductKey,
            dc.CustomerKey,
            dt.TerritoryKey,
            dsp.SalesPersonKey,
            dsm.ShipMethodKey,
            dcc.CreditCardKey,
            dcr.CurrencyRateKey,
            OrderDateKey = CAST(CONVERT(VARCHAR(8), soh.OrderDate, 112) AS INT),
            DueDateKey = CAST(CONVERT(VARCHAR(8), soh.DueDate, 112) AS INT),
            ShipDateKey = CAST(CONVERT(VARCHAR(8), soh.ShipDate, 112) AS INT),
            sod.OrderQty,
            sod.UnitPrice,
            sod.UnitPriceDiscount,
            sod.LineTotal
        FROM AdventureWorks2019.Sales.SalesOrderDetail sod
        INNER JOIN AdventureWorks2019.Sales.SalesOrderHeader soh ON sod.SalesOrderID = soh.SalesOrderID
        INNER JOIN dbo.DimProduct dp ON dp.ProductID_BK = sod.ProductID
        INNER JOIN dbo.DimCustomer dc ON dc.CustomerID_BK = soh.CustomerID
        INNER JOIN dbo.DimTerritory dt ON dt.TerritoryID_BK = soh.TerritoryID
        LEFT JOIN dbo.DimSalesPerson dsp ON dsp.BusinessEntityID_BK = soh.SalesPersonID
        INNER JOIN dbo.DimShipMethod dsm ON dsm.ShipMethodID_BK = soh.ShipMethodID
        LEFT JOIN dbo.DimCreditCard dcc ON dcc.CreditCardID_BK = soh.CreditCardID
        LEFT JOIN dbo.DimCurrencyRate dcr ON dcr.CurrencyRateID_BK = soh.CurrencyRateID
    ) AS source
    ON target.SalesOrderDetailID_Degenerate = source.SalesOrderDetailID_Degenerate
    WHEN MATCHED THEN
        UPDATE SET
            SalesOrderID_Degenerate = source.SalesOrderID_Degenerate,
            SalesOrderNumber_Degenerate = source.SalesOrderNumber_Degenerate,
            ProductKey = source.ProductKey,
            CustomerKey = source.CustomerKey,
            TerritoryKey = source.TerritoryKey,
            SalesPersonKey = source.SalesPersonKey,
            ShipMethodKey = source.ShipMethodKey,
            CreditCardKey = source.CreditCardKey,
            CurrencyRateKey = source.CurrencyRateKey,
            OrderDateKey = source.OrderDateKey,
            DueDateKey = source.DueDateKey,
            ShipDateKey = source.ShipDateKey,
            OrderQty = source.OrderQty,
            UnitPrice = source.UnitPrice,
            UnitPriceDiscount = source.UnitPriceDiscount,
            LineTotal = source.LineTotal
    WHEN NOT MATCHED THEN
        INSERT (
            SalesOrderID_Degenerate, SalesOrderDetailID_Degenerate, SalesOrderNumber_Degenerate, ProductKey,
            CustomerKey, TerritoryKey, SalesPersonKey, ShipMethodKey, CreditCardKey, CurrencyRateKey,
            OrderDateKey, DueDateKey, ShipDateKey, OrderQty, UnitPrice, UnitPriceDiscount, LineTotal)
        VALUES (
            source.SalesOrderID_Degenerate, source.SalesOrderDetailID_Degenerate, source.SalesOrderNumber_Degenerate, source.ProductKey,
            source.CustomerKey, source.TerritoryKey, source.SalesPersonKey, source.ShipMethodKey, source.CreditCardKey, source.CurrencyRateKey,
            source.OrderDateKey, source.DueDateKey, source.ShipDateKey, source.OrderQty, source.UnitPrice, source.UnitPriceDiscount, source.LineTotal);

    SELECT RowsInserted = @@ROWCOUNT, RowsUpdated = 0;
END
GO
