USE [HotelBooking]
GO
/****** Object:  User [HotelUser]    Script Date: 12-10-2025 20:51:40 ******/
CREATE USER [HotelUser] FOR LOGIN [HotelUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [HotelUser]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [HotelUser]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [HotelUser]
GO
/****** Object:  Table [dbo].[content]    Script Date: 12-10-2025 20:51:40 ******/
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
/****** Object:  Table [dbo].[tblavailability]    Script Date: 12-10-2025 20:51:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblavailability](
	[RoomNo] [int] NOT NULL,
	[BookedDate] [nvarchar](23) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCustomers]    Script Date: 12-10-2025 20:51:40 ******/
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
/****** Object:  Table [dbo].[tblreservation]    Script Date: 12-10-2025 20:51:40 ******/
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
/****** Object:  Table [dbo].[tblrooms]    Script Date: 12-10-2025 20:51:40 ******/
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
