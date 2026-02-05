USE AdventureWorksDW_Sales;
GO

CREATE TABLE dbo.DimDate (
    DateKey int NOT NULL PRIMARY KEY,
    FullDate date NOT NULL,
    Year int NOT NULL,
    Quarter int NOT NULL,
    MonthNumber int NOT NULL,
    MonthName nvarchar(20) NOT NULL,
    DayOfMonth int NOT NULL,
    DayName nvarchar(20) NOT NULL,
    IsWeekend bit NOT NULL
);

CREATE TABLE dbo.DimProduct (
    ProductKey int IDENTITY(1,1) PRIMARY KEY,
    ProductID_BK int NOT NULL UNIQUE,
    Name nvarchar(50) NOT NULL,
    ProductNumber nvarchar(25) NOT NULL,
    Color nvarchar(15) NULL,
    StandardCost money NOT NULL,
    ListPrice money NOT NULL,
    Size nvarchar(5) NULL,
    Weight decimal(8,2) NULL,
    SellStartDate datetime NOT NULL,
    SellEndDate datetime NULL,
    DiscontinuedDate datetime NULL
);

CREATE TABLE dbo.DimCustomer (
    CustomerKey int IDENTITY(1,1) PRIMARY KEY,
    CustomerID_BK int NOT NULL UNIQUE,
    AccountNumber nvarchar(10) NOT NULL,
    CustomerName nvarchar(200) NOT NULL,
    PersonType nchar(2) NULL,
    FirstName nvarchar(50) NULL,
    LastName nvarchar(50) NULL,
    TerritoryID_BK int NULL
);

CREATE TABLE dbo.DimTerritory (
    TerritoryKey int IDENTITY(1,1) PRIMARY KEY,
    TerritoryID_BK int NOT NULL UNIQUE,
    Name nvarchar(50) NOT NULL,
    CountryRegionCode nvarchar(3) NOT NULL,
    GroupName nvarchar(50) NOT NULL
);

CREATE TABLE dbo.DimSalesPerson (
    SalesPersonKey int IDENTITY(1,1) PRIMARY KEY,
    BusinessEntityID_BK int NOT NULL UNIQUE,
    TerritoryID_BK int NULL,
    FirstName nvarchar(50) NOT NULL,
    LastName nvarchar(50) NOT NULL,
    SalesQuota money NULL,
    Bonus money NOT NULL,
    CommissionPct smallmoney NOT NULL
);

CREATE TABLE dbo.DimShipMethod (
    ShipMethodKey int IDENTITY(1,1) PRIMARY KEY,
    ShipMethodID_BK int NOT NULL UNIQUE,
    Name nvarchar(50) NOT NULL,
    ShipBase money NOT NULL,
    ShipRate money NOT NULL
);

CREATE TABLE dbo.DimCreditCard (
    CreditCardKey int IDENTITY(1,1) PRIMARY KEY,
    CreditCardID_BK int NOT NULL UNIQUE,
    CardType nvarchar(50) NOT NULL,
    ExpMonth tinyint NOT NULL,
    ExpYear smallint NOT NULL
);

CREATE TABLE dbo.DimCurrencyRate (
    CurrencyRateKey int IDENTITY(1,1) PRIMARY KEY,
    CurrencyRateID_BK int NOT NULL UNIQUE,
    CurrencyRateDate date NOT NULL,
    FromCurrencyCode nchar(3) NOT NULL,
    ToCurrencyCode nchar(3) NOT NULL,
    AverageRate money NOT NULL,
    EndOfDayRate money NOT NULL
);

CREATE TABLE dbo.FactSalesOrderLine (
    SalesOrderLineKey bigint IDENTITY(1,1) PRIMARY KEY,
    SalesOrderID_Degenerate int NOT NULL,
    SalesOrderDetailID_Degenerate int NOT NULL,
    SalesOrderNumber_Degenerate nvarchar(25) NOT NULL,
    ProductKey int NOT NULL,
    CustomerKey int NOT NULL,
    TerritoryKey int NOT NULL,
    SalesPersonKey int NULL,
    ShipMethodKey int NOT NULL,
    CreditCardKey int NULL,
    CurrencyRateKey int NULL,
    OrderDateKey int NOT NULL,
    DueDateKey int NOT NULL,
    ShipDateKey int NULL,
    OrderQty smallint NOT NULL,
    UnitPrice money NOT NULL,
    UnitPriceDiscount money NOT NULL,
    LineTotal money NOT NULL,
    CONSTRAINT FK_Fact_Product FOREIGN KEY (ProductKey) REFERENCES dbo.DimProduct(ProductKey),
    CONSTRAINT FK_Fact_Customer FOREIGN KEY (CustomerKey) REFERENCES dbo.DimCustomer(CustomerKey),
    CONSTRAINT FK_Fact_Territory FOREIGN KEY (TerritoryKey) REFERENCES dbo.DimTerritory(TerritoryKey),
    CONSTRAINT FK_Fact_SalesPerson FOREIGN KEY (SalesPersonKey) REFERENCES dbo.DimSalesPerson(SalesPersonKey),
    CONSTRAINT FK_Fact_ShipMethod FOREIGN KEY (ShipMethodKey) REFERENCES dbo.DimShipMethod(ShipMethodKey),
    CONSTRAINT FK_Fact_CreditCard FOREIGN KEY (CreditCardKey) REFERENCES dbo.DimCreditCard(CreditCardKey),
    CONSTRAINT FK_Fact_CurrencyRate FOREIGN KEY (CurrencyRateKey) REFERENCES dbo.DimCurrencyRate(CurrencyRateKey),
    CONSTRAINT FK_Fact_OrderDate FOREIGN KEY (OrderDateKey) REFERENCES dbo.DimDate(DateKey),
    CONSTRAINT FK_Fact_DueDate FOREIGN KEY (DueDateKey) REFERENCES dbo.DimDate(DateKey),
    CONSTRAINT FK_Fact_ShipDate FOREIGN KEY (ShipDateKey) REFERENCES dbo.DimDate(DateKey)
);
