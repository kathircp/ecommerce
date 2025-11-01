-- Drop existing objects if present (safe when iterating)
IF OBJECT_ID('dbo.UserRoles') IS NOT NULL DROP TABLE dbo.UserRoles;
IF OBJECT_ID('dbo.Roles') IS NOT NULL DROP TABLE dbo.Roles;
IF OBJECT_ID('dbo.OrderItems') IS NOT NULL DROP TABLE dbo.OrderItems;
IF OBJECT_ID('dbo.Orders') IS NOT NULL DROP TABLE dbo.Orders;
IF OBJECT_ID('dbo.Users') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Products') IS NOT NULL DROP TABLE dbo.Products;
IF OBJECT_ID('dbo.OrdersWithTotals') IS NOT NULL DROP VIEW dbo.OrdersWithTotals;

-- Products
CREATE TABLE dbo.Products
(
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Products PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- Users
CREATE TABLE dbo.Users
(
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Users PRIMARY KEY DEFAULT NEWID(),
    Username NVARCHAR(100) NOT NULL CONSTRAINT UQ_Users_Username UNIQUE,
    -- In production store a salted hash, not plain text
    PasswordHash NVARCHAR(512) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- Roles
CREATE TABLE dbo.Roles
(
    Id INT NOT NULL IDENTITY(1,1) CONSTRAINT PK_Roles PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL CONSTRAINT UQ_Roles_Name UNIQUE
);

-- UserRoles (many-to-many)
CREATE TABLE dbo.UserRoles
(
    UserId UNIQUEIDENTIFIER NOT NULL,
    RoleId INT NOT NULL,
    CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleId),
    CONSTRAINT FK_UserRoles_User FOREIGN KEY (UserId) REFERENCES dbo.Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UserRoles_Role FOREIGN KEY (RoleId) REFERENCES dbo.Roles(Id) ON DELETE CASCADE
);

-- Orders
CREATE TABLE dbo.Orders
(
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_Orders PRIMARY KEY DEFAULT NEWID(),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    -- Store total for convenience (application can maintain it) - keep in sync by app or trigger
    Total DECIMAL(18,2) NOT NULL DEFAULT (0.00),
    CustomerId UNIQUEIDENTIFIER NULL  -- optional FK to Users.Id if you want to track customer
);
-- Optional FK to Users for customer:
ALTER TABLE dbo.Orders ADD CONSTRAINT FK_Orders_Customer FOREIGN KEY (CustomerId) REFERENCES dbo.Users(Id);

-- OrderItems
CREATE TABLE dbo.OrderItems
(
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_OrderItems PRIMARY KEY DEFAULT NEWID(),
    OrderId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NULL,
    ProductName NVARCHAR(200) NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL,
    LineTotal AS (UnitPrice * Quantity) PERSISTED,
    CONSTRAINT FK_OrderItems_Order FOREIGN KEY (OrderId) REFERENCES dbo.Orders(Id) ON DELETE CASCADE,
    CONSTRAINT FK_OrderItems_Product FOREIGN KEY (ProductId) REFERENCES dbo.Products(Id) ON DELETE NO ACTION
);

-- Indexes for FK lookup performance
CREATE INDEX IX_OrderItems_OrderId ON dbo.OrderItems(OrderId);
CREATE INDEX IX_OrderItems_ProductId ON dbo.OrderItems(ProductId);
CREATE INDEX IX_Orders_CustomerId ON dbo.Orders(CustomerId);

-- View that returns orders with computed totals (sums order items)
CREATE VIEW dbo.OrdersWithTotals
AS
SELECT
    o.Id,
    o.CreatedAt,
    o.CustomerId,
    TotalFromItems = ISNULL(SUM(oi.LineTotal), 0.00)
FROM dbo.Orders o
LEFT JOIN dbo.OrderItems oi ON oi.OrderId = o.Id
GROUP BY o.Id, o.CreatedAt, o.CustomerId;
GO

-- Sample data (optional)
INSERT INTO dbo.Products (Name, Description, Price, Stock) VALUES
('Red T-Shirt', 'Comfortable cotton tee', 19.99, 50),
('Sneakers', 'Running shoes', 89.50, 20);

INSERT INTO dbo.Roles (Name) VALUES ('Admin'), ('User');

-- Example demo users (passwords here are placeholder plain text; replace with proper hashes)
INSERT INTO dbo.Users (Username, PasswordHash) VALUES
('admin', 'admin123'),
('user', 'user123');

-- Assign roles
INSERT INTO dbo.UserRoles (UserId, RoleId)
SELECT u.Id, r.Id
FROM dbo.Users u
JOIN dbo.Roles r ON r.Name = CASE WHEN u.Username = 'admin' THEN 'Admin' ELSE 'User' END;

-- Example order with items
DECLARE @p1 UNIQUEIDENTIFIER = (SELECT TOP(1) Id FROM dbo.Products WHERE Name = 'Red T-Shirt');
DECLARE @p2 UNIQUEIDENTIFIER = (SELECT TOP(1) Id FROM dbo.Products WHERE Name = 'Sneakers');
DECLARE @orderId UNIQUEIDENTIFIER = NEWID();
INSERT INTO dbo.Orders (Id, CreatedAt, Total)
VALUES (@orderId, SYSUTCDATETIME(), 0.00);

INSERT INTO dbo.OrderItems (OrderId, ProductId, ProductName, UnitPrice, Quantity)
VALUES
(@orderId, @p1, 'Red T-Shirt', 19.99, 2),
(@orderId, @p2, 'Sneakers', 89.50, 1);

-- Update Orders.Total from items (one-time sync example)
UPDATE o
SET Total = s.TotalFromItems
FROM dbo.Orders o
JOIN (
    SELECT OrderId, SUM(LineTotal) AS TotalFromItems
    FROM dbo.OrderItems
    GROUP BY OrderId
) s ON s.OrderId = o.Id;