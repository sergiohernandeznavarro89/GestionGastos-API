USE [gestionGastos-db]
GO
/****** Object:  User [gestionGastosApi]    Script Date: 16/09/2026 8:37:51 ******/
CREATE USER [gestionGastosApi] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [gestionGastosApi]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [gestionGastosApi]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](200) NOT NULL,
	[UserId] [int] NOT NULL,
	[Ammount] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AmmountType]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AmmountType](
	[AmmountTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AmmountTypeDesc] [nvarchar](250) NOT NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AmmountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryDesc] [nvarchar](300) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Debt]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Debt](
	[DebtId] [int] IDENTITY(1,1) NOT NULL,
	[DebtName] [nvarchar](250) NOT NULL,
	[StartAmount] [decimal](18, 2) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[SubCategoryId] [int] NOT NULL,
	[DebtTypeId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[DebtorName] [nvarchar](250) NULL,
	[CurrentAmount] [decimal](18, 2) NOT NULL,
	[CompletedDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[DebtId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DebtPayment]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DebtPayment](
	[DebtPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[DebtId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DebtPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DebtType]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DebtType](
	[DebtTypeId] [int] NOT NULL,
	[DebtTypeDesc] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_DebtType] PRIMARY KEY CLUSTERED 
(
	[DebtTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](250) NOT NULL,
	[ItemDesc] [nvarchar](max) NULL,
	[Ammount] [decimal](18, 2) NOT NULL,
	[Periodity] [int] NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Cancelled] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[SubCategoryId] [int] NULL,
	[ItemTypeId] [int] NOT NULL,
	[AmmountTypeId] [int] NOT NULL,
	[PeriodTypeId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemPayment]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemPayment](
	[ItemPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[Ammount] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemType]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemType](
	[ItemTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ItemTypeDesc] [nvarchar](250) NOT NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PeriodType]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeriodType](
	[PeriodTypeId] [int] IDENTITY(1,1) NOT NULL,
	[PeriodTypeDesc] [nvarchar](250) NOT NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PeriodTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[SubCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[SubCategoryDesc] [nvarchar](300) NOT NULL,
	[CategoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transfer]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transfer](
	[TransferId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[SubCategoryId] [int] NULL,
	[PeriodTypeId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[OriginAccountId] [int] NOT NULL,
	[DestinationAccountId] [int] NOT NULL,
	[TransferName] [nvarchar](100) NOT NULL,
	[TransferDesc] [nvarchar](max) NULL,
	[Ammount] [decimal](18, 2) NOT NULL,
	[Periodity] [int] NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Cancelled] [bit] NOT NULL,
 CONSTRAINT [PK_Transfer] PRIMARY KEY CLUSTERED 
(
	[TransferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferPayment]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferPayment](
	[TransferPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[TransferId] [int] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[Ammount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TransferPayment] PRIMARY KEY CLUSTERED 
(
	[TransferPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16/09/2026 8:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](150) NOT NULL,
	[UserLastName] [nvarchar](150) NOT NULL,
	[UserPass] [nvarchar](200) NOT NULL,
	[UserEmail] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [Ammount]
GO
ALTER TABLE [dbo].[Item] ADD  DEFAULT ((0)) FOR [Cancelled]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_User]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_User]
GO
ALTER TABLE [dbo].[Debt]  WITH CHECK ADD  CONSTRAINT [FK_Debt_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Debt] CHECK CONSTRAINT [FK_Debt_Category]
GO
ALTER TABLE [dbo].[Debt]  WITH CHECK ADD  CONSTRAINT [FK_Debt_SubCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[SubCategory] ([SubCategoryId])
GO
ALTER TABLE [dbo].[Debt] CHECK CONSTRAINT [FK_Debt_SubCategory]
GO
ALTER TABLE [dbo].[Debt]  WITH CHECK ADD  CONSTRAINT [FK_Debt_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Debt] CHECK CONSTRAINT [FK_Debt_User]
GO
ALTER TABLE [dbo].[DebtPayment]  WITH CHECK ADD  CONSTRAINT [FK_DebtPayment_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[DebtPayment] CHECK CONSTRAINT [FK_DebtPayment_Account]
GO
ALTER TABLE [dbo].[DebtPayment]  WITH CHECK ADD  CONSTRAINT [FK_DebtPayment_Debt] FOREIGN KEY([DebtId])
REFERENCES [dbo].[Debt] ([DebtId])
GO
ALTER TABLE [dbo].[DebtPayment] CHECK CONSTRAINT [FK_DebtPayment_Debt]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Account]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Category]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_SubCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[SubCategory] ([SubCategoryId])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_SubCategory]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_User]
GO
ALTER TABLE [dbo].[ItemPayment]  WITH CHECK ADD  CONSTRAINT [FK_ItemPayment_Item] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([ItemId])
GO
ALTER TABLE [dbo].[ItemPayment] CHECK CONSTRAINT [FK_ItemPayment_Item]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Category]
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_Category]
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_DestinationAccount] FOREIGN KEY([DestinationAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_DestinationAccount]
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_OriginAccount] FOREIGN KEY([OriginAccountId])
REFERENCES [dbo].[Account] ([AccountId])
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_OriginAccount]
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_User]
GO
ALTER TABLE [dbo].[TransferPayment]  WITH CHECK ADD  CONSTRAINT [FK_TransferPayment_Transfer] FOREIGN KEY([TransferId])
REFERENCES [dbo].[Transfer] ([TransferId])
GO
ALTER TABLE [dbo].[TransferPayment] CHECK CONSTRAINT [FK_TransferPayment_Transfer]
GO
