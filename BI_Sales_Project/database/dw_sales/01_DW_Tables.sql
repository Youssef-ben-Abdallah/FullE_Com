USE AdventureWorksDW_Sales;
GO

CREATE TABLE dbo.DimDate (
    DateKey INT PRIMARY KEY,
    FullDate DATE NOT NULL,
    Year INT NOT NULL,
    Quarter INT NOT NULL,
    MonthNumber INT NOT NULL,
    MonthName NVARCHAR(20) NOT NULL,
    DayOfMonth INT NOT NULL,
    DayName NVARCHAR(20) NOT NULL,
    IsWeekend BIT NOT NULL
);

CREATE TABLE dbo.DimProduct (
    ProductKey INT IDENTITY PRIMARY KEY,
    ProductID_BK INT NOT NULL UNIQUE,
    Name NVARCHAR(50) NOT NULL,
    ProductNumber NVARCHAR(25) NOT NULL,
    Color NVARCHAR(15) NULL,
    StandardCost MONEY NOT NULL,
    ListPrice MONEY NOT NULL,
    Size NVARCHAR(5) NULL,
    Weight DECIMAL(8,2) NULL,
    SellStartDate DATETIME NOT NULL,
    SellEndDate DATETIME NULL,
    DiscontinuedDate DATETIME NULL
);

CREATE TABLE dbo.DimCustomer (
    CustomerKey INT IDENTITY PRIMARY KEY,
    CustomerID_BK INT NOT NULL UNIQUE,
    AccountNumber NVARCHAR(10) NOT NULL,
    CustomerName NVARCHAR(200) NOT NULL,
    PersonType NCHAR(2) NULL,
    FirstName NVARCHAR(50) NULL,
    LastName NVARCHAR(50) NULL,
    TerritoryID_BK INT NULL
);

CREATE TABLE dbo.DimTerritory (
    TerritoryKey INT IDENTITY PRIMARY KEY,
    TerritoryID_BK INT NOT NULL UNIQUE,
    Name NVARCHAR(50) NOT NULL,
    CountryRegionCode NVARCHAR(3) NOT NULL,
    GroupName NVARCHAR(50) NOT NULL
);

CREATE TABLE dbo.DimSalesPerson (
    SalesPersonKey INT IDENTITY PRIMARY KEY,
    BusinessEntityID_BK INT NOT NULL UNIQUE,
    TerritoryID_BK INT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    SalesQuota MONEY NULL,
    Bonus MONEY NOT NULL,
    CommissionPct SMALLMONEY NOT NULL
);

CREATE TABLE dbo.DimShipMethod (
    ShipMethodKey INT IDENTITY PRIMARY KEY,
    ShipMethodID_BK INT NOT NULL UNIQUE,
    Name NVARCHAR(50) NOT NULL,
    ShipBase MONEY NOT NULL,
    ShipRate MONEY NOT NULL
);

CREATE TABLE dbo.DimCreditCard (
    CreditCardKey INT IDENTITY PRIMARY KEY,
    CreditCardID_BK INT NOT NULL UNIQUE,
    CardType NVARCHAR(50) NOT NULL,
    ExpMonth TINYINT NOT NULL,
    ExpYear SMALLINT NOT NULL
);

CREATE TABLE dbo.DimCurrencyRate (
    CurrencyRateKey INT IDENTITY PRIMARY KEY,
    CurrencyRateID_BK INT NOT NULL UNIQUE,
    CurrencyRateDate DATE NOT NULL,
    FromCurrencyCode NCHAR(3) NOT NULL,
    ToCurrencyCode NCHAR(3) NOT NULL,
    AverageRate MONEY NOT NULL,
    EndOfDayRate MONEY NOT NULL
);

CREATE TABLE dbo.FactSalesOrderLine (
    SalesOrderLineKey BIGINT IDENTITY PRIMARY KEY,
    SalesOrderID_Degenerate INT NOT NULL,
    SalesOrderDetailID_Degenerate INT NOT NULL,
    SalesOrderNumber_Degenerate NVARCHAR(25) NOT NULL,
    ProductKey INT NOT NULL,
    CustomerKey INT NOT NULL,
    TerritoryKey INT NOT NULL,
    SalesPersonKey INT NULL,
    ShipMethodKey INT NOT NULL,
    CreditCardKey INT NULL,
    CurrencyRateKey INT NULL,
    OrderDateKey INT NOT NULL,
    DueDateKey INT NOT NULL,
    ShipDateKey INT NULL,
    OrderQty SMALLINT NOT NULL,
    UnitPrice MONEY NOT NULL,
    UnitPriceDiscount MONEY NOT NULL,
    LineTotal MONEY NOT NULL,
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
