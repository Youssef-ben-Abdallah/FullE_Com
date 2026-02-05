CREATE DATABASE OltpEcommerceDb;
GO
USE OltpEcommerceDb;
GO

CREATE TABLE dbo.Category (
    CategoryId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(80) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL
);

CREATE TABLE dbo.SubCategory (
    SubCategoryId INT IDENTITY PRIMARY KEY,
    CategoryId INT NOT NULL,
    Name NVARCHAR(80) NOT NULL,
    Description NVARCHAR(255) NULL,
    CONSTRAINT FK_SubCategory_Category FOREIGN KEY (CategoryId) REFERENCES dbo.Category(CategoryId)
);

CREATE TABLE dbo.Product (
    ProductId INT IDENTITY PRIMARY KEY,
    SubCategoryId INT NOT NULL,
    Name NVARCHAR(120) NOT NULL,
    Sku NVARCHAR(30) NOT NULL UNIQUE,
    UnitPrice DECIMAL(18,2) NOT NULL,
    StockQty INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT(1),
    ImageUrl NVARCHAR(400) NULL,
    CONSTRAINT FK_Product_SubCategory FOREIGN KEY (SubCategoryId) REFERENCES dbo.SubCategory(SubCategoryId)
);

CREATE TABLE dbo.ShoppingCart (
    CartId UNIQUEIDENTIFIER PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);

CREATE TABLE dbo.CartItem (
    CartItemId INT IDENTITY PRIMARY KEY,
    CartId UNIQUEIDENTIFIER NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPriceSnapshot DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_CartItem_Cart FOREIGN KEY (CartId) REFERENCES dbo.ShoppingCart(CartId),
    CONSTRAINT FK_CartItem_Product FOREIGN KEY (ProductId) REFERENCES dbo.Product(ProductId)
);

CREATE TABLE dbo.[Order] (
    OrderId INT IDENTITY PRIMARY KEY,
    OrderNumber NVARCHAR(30) NOT NULL UNIQUE,
    UserId NVARCHAR(450) NOT NULL,
    CreatedAt DATETIME2 NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    SubTotal DECIMAL(18,2) NOT NULL,
    ShippingFee DECIMAL(18,2) NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    Notes NVARCHAR(400) NULL
);

CREATE TABLE dbo.OrderItem (
    OrderItemId INT IDENTITY PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPriceSnapshot DECIMAL(18,2) NOT NULL,
    LineTotal DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderItem_Order FOREIGN KEY (OrderId) REFERENCES dbo.[Order](OrderId),
    CONSTRAINT FK_OrderItem_Product FOREIGN KEY (ProductId) REFERENCES dbo.Product(ProductId)
);
