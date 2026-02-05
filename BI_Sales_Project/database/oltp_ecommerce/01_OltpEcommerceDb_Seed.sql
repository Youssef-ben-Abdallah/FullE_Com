USE OltpEcommerceDb;
GO

INSERT INTO dbo.Category (Name, Description) VALUES
('Electronics', 'Devices and gadgets'),
('Home', 'Home and kitchen');

INSERT INTO dbo.SubCategory (CategoryId, Name, Description) VALUES
(1, 'Laptops', 'Portable computers'),
(1, 'Phones', 'Smartphones'),
(2, 'Kitchen', 'Kitchen appliances');

INSERT INTO dbo.Product (SubCategoryId, Name, Sku, UnitPrice, StockQty, IsActive, ImageUrl) VALUES
(1, 'Laptop Pro', 'SKU-LAP-01', 1500.00, 25, 1, NULL),
(2, 'Smartphone X', 'SKU-PHN-01', 899.99, 50, 1, NULL),
(3, 'Blender', 'SKU-KIT-01', 129.99, 40, 1, NULL);
