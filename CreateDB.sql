USE [Shop]
GO
/****** Object:  Table [dbo].[Catalogs]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CatalogName] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumberOrder] [nvarchar](15) NOT NULL,
	[Date] [date] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdersProducts]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProductCount] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Article] [nvarchar](50) NULL,
	[NameProduct] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[CatalogId] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[FIO] [nvarchar](max) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[OrdersProducts]  WITH CHECK ADD  CONSTRAINT [FK_OrdersProducts_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrdersProducts] CHECK CONSTRAINT [FK_OrdersProducts_Orders]
GO
ALTER TABLE [dbo].[OrdersProducts]  WITH CHECK ADD  CONSTRAINT [FK_OrdersProducts_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[OrdersProducts] CHECK CONSTRAINT [FK_OrdersProducts_Products]
GO
/****** Object:  StoredProcedure [dbo].[GetSales]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSales]
AS
SELECT Products.NameProduct,  SUM(OrdersProducts.ProductCount) AS Quantity, SUM((OrdersProducts.ProductCount)*Products.Price) AS Amount FROM Products INNER JOIN OrdersProducts
ON Products.Id = OrdersProducts.ProductId INNER JOIN Orders
ON OrdersProducts.OrderId = Orders.Id
GROUP BY Products.NameProduct
GO
/****** Object:  StoredProcedure [dbo].[InsertOrders]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertOrders]
@XmlPath nvarchar(max)
AS
declare @x xml;
declare @cmd nvarchar(max) = N'select @xml = cast(t.data as xml) from openrowset(bulk ' + quotename(@XmlPath, N'''') + N', single_blob) t(data)';
exec sp_executesql @cmd, N'@xml xml output', @x output;
INSERT INTO Orders(NumberOrder,Date,UserId)
SELECT
A.OrderShop.query('no').value('.','nvarchar(15)') AS NumberOrder,
A.OrderShop.query('reg_date').value('.','date') AS Date,
(SELECT Users.Id FROM Users Where Users.email = (SELECT A.OrderShop.query('user/email').value('.','nvarchar(50)') AS email)) AS UserId
FROM
(
SELECT @x
) AS S(c)
cross apply c.nodes('/orders/order') AS A(OrderShop) 
WHERE NOT EXISTS(SELECT * FROM Orders
				WHERE NumberOrder = Orders.NumberOrder)
GO
/****** Object:  StoredProcedure [dbo].[InsertOrdersProducts]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertOrdersProducts]
@XmlPath nvarchar(max)
AS
declare @x xml;
declare @cmd nvarchar(max) = N'select @xml = cast(t.data as xml) from openrowset(bulk ' + quotename(@XmlPath, N'''') + N', single_blob) t(data)';
exec sp_executesql @cmd, N'@xml xml output', @x output;
INSERT INTO OrdersProducts(OrderId,ProductId,ProductCount)
SELECT
(SELECT Orders.Id FROM Orders Where Orders.NumberOrder = (SELECT A.OrderProduct.query('../no').value('.','nvarchar(15)') AS NumberOrder)) AS OrderId,
(SELECT Products.Id FROM Products Where Products.NameProduct = (SELECT A.OrderProduct.query('name').value('.','nvarchar(50)') AS NameProduct) 
									AND Products.Price = (SELECT A.OrderProduct.query('price').value('.','float') AS Price) ) AS ProductId,
A.OrderProduct.query('quantity').value('.','float') AS ProductCount
FROM
(
SELECT @x
) AS S(c)
cross apply c.nodes('./orders/order/product') AS A(OrderProduct) 
WHERE NOT EXISTS(SELECT * FROM OrdersProducts
				WHERE OrderId = OrdersProducts.OrderId AND ProductId = OrdersProducts.ProductId AND ProductCount = OrdersProducts.ProductCount)
GO
/****** Object:  StoredProcedure [dbo].[InsertProducts]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertProducts]
@XmlPath nvarchar(max)
AS
declare @x xml;
declare @cmd nvarchar(max) = N'select @xml = cast(t.data as xml) from openrowset(bulk ' + quotename(@XmlPath, N'''') + N', single_blob) t(data)';
exec sp_executesql @cmd, N'@xml xml output', @x output;
INSERT INTO Products(NameProduct,Price)
SELECT DISTINCT
A.Product.query('name').value('.','nvarchar(50)') AS NameProduct,
A.Product.query('price').value('.','float') AS Price
FROM
(
select @x 
) 
AS S(c)
cross apply c.nodes('/orders/order/product') AS A(Product) 
WHERE NOT EXISTS(SELECT * FROM Products
				WHERE NameProduct =Products.NameProduct AND Price = Products.Price)
GO
/****** Object:  StoredProcedure [dbo].[InsertUsers]    Script Date: 09.08.2024 20:44:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUsers]
@XmlPath nvarchar(max)
AS
declare @x xml;
declare @cmd nvarchar(max) = N'select @xml = cast(t.data as xml) from openrowset(bulk ' + quotename(@XmlPath, N'''') + N', single_blob) t(data)';
exec sp_executesql @cmd, N'@xml xml output', @x output;
INSERT INTO Users(FIO,email)
SELECT DISTINCT
A.UserShop.query('fio').value('.','nvarchar(MAX)') AS FIO,
A.UserShop.query('email').value('.','nvarchar(50)') AS email
FROM
(
select @x
) AS S(c)
cross apply c.nodes('/orders/order/user') AS A(UserShop) 
WHERE NOT EXISTS(SELECT * FROM Users
				WHERE email =Users.email)
GO
