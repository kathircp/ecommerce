USE [Ecommerce]
GO
/****** Object:  Table [dbo].[content]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[content](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[title1] [nvarchar](160) NOT NULL,
	[title2] [nvarchar](160) NOT NULL,
	[value] [nvarchar](max) NOT NULL,
	[hours] [nvarchar](max) NOT NULL,
	[phonenumber] [nvarchar](max) NOT NULL,
	[phonenumber1] [nvarchar](60) NOT NULL,
	[faxnumber] [nvarchar](60) NOT NULL,
	[address] [nvarchar](max) NOT NULL,
	[description] [nvarchar](1500) NOT NULL,
	[mail] [nvarchar](50) NOT NULL,
	[whours] [datetime] NOT NULL,
	[contact] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblavailability]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblavailability](
	[RoomNo] [int] NOT NULL,
	[BookedDate] [nvarchar](23) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCustomers]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCustomers](
	[CustID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[PhoneNo] [nvarchar](13) NOT NULL,
	[Age] [int] NOT NULL,
	[Sex] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[CustID] ASC,
	[Name] ASC,
	[PhoneNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblreservation]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblreservation](
	[CustID] [int] IDENTITY(1,1) NOT NULL,
	[RoomNo] [int] NOT NULL,
	[CheckInDate] [nvarchar](10) NOT NULL,
	[CheckOutDate] [nvarchar](10) NOT NULL,
	[NoOfAdults] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblrooms]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblrooms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoomNo] [int] NOT NULL,
	[RoomType] [nvarchar](20) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[Cart]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[Cart](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ImageUrl] [nvarchar](200) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[Quantity] [int] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[Category]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IndexPageId] [int] NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[SubCategoryId] [int] NULL,
	[Rank] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[FilterBy]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[FilterBy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilterName] [nvarchar](50) NOT NULL,
	[OptionName] [nvarchar](50) NULL,
	[OptionValue] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[IndexPage]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[IndexPage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleName] [nvarchar](50) NOT NULL,
	[FilterById] [nvarchar](50) NULL,
	[Rank] [int] NULL,
 CONSTRAINT [PK_IndexPage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[OrderItems]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[OrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[LineTotal]  AS ([UnitPrice]*[Quantity]) PERSISTED,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[Orders]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[CustomerId] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[Payment]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Currency] [nvarchar](3) NOT NULL,
	[TransactionId] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[TransactionDesc] [nvarchar](250) NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[Products]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CategoryId] [int] NULL,
	[Color] [nvarchar](10) NULL,
	[Discount] [int] NULL,
	[Blouse] [bit] NULL,
	[NewArrival] [bit] NULL,
	[ImageUrl] [nvarchar](512) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[Shipment]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[Shipment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[PaymentId] [int] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[OrderAmount] [decimal](10, 2) NOT NULL,
	[ShipAmount] [decimal](10, 2) NOT NULL,
	[TotalAmount] [decimal](10, 2) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
	[DeliveryAddressId] [int] NOT NULL,
 CONSTRAINT [PK__Shipment__3214EC07ED1EE4F9] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[Track]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[Track](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[PaymentId] [int] NOT NULL,
	[ShipmentId] [int] NOT NULL,
	[DeliveryAddressId] [int] NOT NULL,
	[PackageVendorName] [nvarchar](100) NOT NULL,
	[PackageVendorAddress] [nvarchar](100) NOT NULL,
	[PackageStatus] [nvarchar](30) NOT NULL,
	[DeliveryStatus] [nvarchar](30) NOT NULL,
	[DeliveryRemarks] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[UserDetail]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[UserDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](15) NOT NULL,
	[Address] [nvarchar](250) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](10) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[AlternatePhoneNo] [nvarchar](15) NULL,
 CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ecomm].[Users]    Script Date: 02-11-2025 22:46:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ecomm].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](512) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Role] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Users_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblavailability] ADD  DEFAULT ((0)) FOR [RoomNo]
GO
ALTER TABLE [dbo].[tblavailability] ADD  DEFAULT (N'') FOR [BookedDate]
GO
ALTER TABLE [dbo].[tblreservation] ADD  DEFAULT ((0)) FOR [RoomNo]
GO
ALTER TABLE [dbo].[tblreservation] ADD  DEFAULT (N'') FOR [CheckInDate]
GO
ALTER TABLE [dbo].[tblreservation] ADD  DEFAULT (N'') FOR [CheckOutDate]
GO
ALTER TABLE [dbo].[tblreservation] ADD  DEFAULT ((0)) FOR [NoOfAdults]
GO
ALTER TABLE [dbo].[tblrooms] ADD  DEFAULT ((0)) FOR [RoomNo]
GO
ALTER TABLE [dbo].[tblrooms] ADD  DEFAULT (N'') FOR [RoomType]
GO
ALTER TABLE [dbo].[tblrooms] ADD  DEFAULT ((0.00)) FOR [Price]
GO
ALTER TABLE [ecomm].[Orders] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [ecomm].[Orders] ADD  DEFAULT ((0.00)) FOR [Total]
GO
ALTER TABLE [ecomm].[Products] ADD  CONSTRAINT [DF__Products__Create__0C50D423]  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [ecomm].[Users] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]
GO
ALTER TABLE [ecomm].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_User] FOREIGN KEY([CustomerId])
REFERENCES [ecomm].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [ecomm].[Cart] CHECK CONSTRAINT [FK_Cart_User]
GO
ALTER TABLE [ecomm].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Order] FOREIGN KEY([OrderId])
REFERENCES [ecomm].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [ecomm].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Order]
GO
ALTER TABLE [ecomm].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Product] FOREIGN KEY([ProductId])
REFERENCES [ecomm].[Products] ([Id])
GO
ALTER TABLE [ecomm].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Product]
GO
ALTER TABLE [ecomm].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customer] FOREIGN KEY([CustomerId])
REFERENCES [ecomm].[Users] ([Id])
GO
ALTER TABLE [ecomm].[Orders] CHECK CONSTRAINT [FK_Orders_Customer]
GO
ALTER TABLE [ecomm].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Orders] FOREIGN KEY([OrderId])
REFERENCES [ecomm].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [ecomm].[Payment] CHECK CONSTRAINT [FK_Payment_Orders]
GO
ALTER TABLE [ecomm].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Users] FOREIGN KEY([CustomerId])
REFERENCES [ecomm].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [ecomm].[Payment] CHECK CONSTRAINT [FK_Payment_Users]
GO
ALTER TABLE [ecomm].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_Shipment_Orders] FOREIGN KEY([OrderId])
REFERENCES [ecomm].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [ecomm].[Shipment] CHECK CONSTRAINT [FK_Shipment_Orders]
GO
ALTER TABLE [ecomm].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_Shipment_Payment] FOREIGN KEY([PaymentId])
REFERENCES [ecomm].[Payment] ([Id])
GO
ALTER TABLE [ecomm].[Shipment] CHECK CONSTRAINT [FK_Shipment_Payment]
GO
ALTER TABLE [ecomm].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_Shipment_UserDetail] FOREIGN KEY([DeliveryAddressId])
REFERENCES [ecomm].[UserDetail] ([Id])
GO
ALTER TABLE [ecomm].[Shipment] CHECK CONSTRAINT [FK_Shipment_UserDetail]
GO
ALTER TABLE [ecomm].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_Shipment_Users] FOREIGN KEY([CustomerId])
REFERENCES [ecomm].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [ecomm].[Shipment] CHECK CONSTRAINT [FK_Shipment_Users]
GO
ALTER TABLE [ecomm].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_Orders] FOREIGN KEY([OrderId])
REFERENCES [ecomm].[Orders] ([Id])
GO
ALTER TABLE [ecomm].[Track] CHECK CONSTRAINT [FK_Track_Orders]
GO
ALTER TABLE [ecomm].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_Payment] FOREIGN KEY([PaymentId])
REFERENCES [ecomm].[Payment] ([Id])
GO
ALTER TABLE [ecomm].[Track] CHECK CONSTRAINT [FK_Track_Payment]
GO
ALTER TABLE [ecomm].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_Shipment] FOREIGN KEY([ShipmentId])
REFERENCES [ecomm].[Shipment] ([Id])
GO
ALTER TABLE [ecomm].[Track] CHECK CONSTRAINT [FK_Track_Shipment]
GO
ALTER TABLE [ecomm].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_userDetail] FOREIGN KEY([DeliveryAddressId])
REFERENCES [ecomm].[UserDetail] ([Id])
GO
ALTER TABLE [ecomm].[Track] CHECK CONSTRAINT [FK_Track_userDetail]
GO
ALTER TABLE [ecomm].[Track]  WITH CHECK ADD  CONSTRAINT [FK_track_Users] FOREIGN KEY([CustomerId])
REFERENCES [ecomm].[Users] ([Id])
GO
ALTER TABLE [ecomm].[Track] CHECK CONSTRAINT [FK_track_Users]
GO
ALTER TABLE [ecomm].[UserDetail]  WITH CHECK ADD  CONSTRAINT [FK_UserDetail_User] FOREIGN KEY([UserId])
REFERENCES [ecomm].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [ecomm].[UserDetail] CHECK CONSTRAINT [FK_UserDetail_User]
GO
