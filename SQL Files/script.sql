USE [master]
GO
/****** Object:  Database [Restaurant]    Script Date: 12-11-2021 11:33:33 ******/
CREATE DATABASE [Restaurant]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Restaurant', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Restaurant.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Restaurant_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Restaurant_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Restaurant] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Restaurant].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Restaurant] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Restaurant] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Restaurant] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Restaurant] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Restaurant] SET ARITHABORT OFF 
GO
ALTER DATABASE [Restaurant] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Restaurant] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Restaurant] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Restaurant] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Restaurant] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Restaurant] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Restaurant] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Restaurant] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Restaurant] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Restaurant] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Restaurant] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Restaurant] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Restaurant] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Restaurant] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Restaurant] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Restaurant] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Restaurant] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Restaurant] SET RECOVERY FULL 
GO
ALTER DATABASE [Restaurant] SET  MULTI_USER 
GO
ALTER DATABASE [Restaurant] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Restaurant] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Restaurant] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Restaurant] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Restaurant] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Restaurant] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Restaurant', N'ON'
GO
ALTER DATABASE [Restaurant] SET QUERY_STORE = OFF
GO
USE [Restaurant]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF;
GO
USE [Restaurant]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateItemPrice]    Script Date: 12-11-2021 11:33:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CalculateItemPrice](@MenuItemID int)
RETURNS float
AS 
BEGIN
		Declare @ItemPrice float
		select @ItemPrice = ItemPrice from RestaurantMenuItem
		where MenuItemID=@MenuItemID
		return @ItemPrice
END;
GO
/****** Object:  Table [dbo].[OrderInfo]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInfo](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[RestuarantID] [int] NOT NULL,
	[MenuItemID] [int] NOT NULL,
	[ItemQuantity] [int] NOT NULL,
	[OrderAmount] [float] NOT NULL,
	[DiningTableID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[BillsTableFunction]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from dbo.BillsTableFunction(21)
--select * from OrderInfo

CREATE FUNCTION [dbo].[BillsTableFunction]
(
@OrderID int
)
RETURNS TABLE
AS
RETURN
(
	SELECT OrderID, OrderDate, RestuarantID, MenuItemID, ItemQuantity,OrderAmount, DiningTableID from OrderInfo where OrderID=@OrderID
)
GO
/****** Object:  Table [dbo].[Cuisine]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuisine](
	[CuisineID] [int] IDENTITY(1,1) NOT NULL,
	[RestuarantID] [int] NULL,
	[CuisineName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CuisineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantMenuItem]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantMenuItem](
	[MenuItemID] [int] IDENTITY(1,1) NOT NULL,
	[CuisineID] [int] NOT NULL,
	[ItemName] [nvarchar](100) NOT NULL,
	[ItemPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[MenuItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ItemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Vw_CuisineDetails]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Vw_CuisineDetails]
AS
SELECT        C.CuisineName, R.ItemName, R.ItemPrice
FROM            dbo.Cuisine AS C INNER JOIN
                         dbo.RestaurantMenuItem AS R ON R.CuisineID = C.CuisineID
GO
/****** Object:  Table [dbo].[Bills]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bills](
	[BillsID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[RestuarantID] [int] NOT NULL,
	[BillAmount] [float] NOT NULL,
	[CustomerID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BillsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[RestuarantID] [int] NOT NULL,
	[CustomerName] [nvarchar](100) NOT NULL,
	[MobileNo] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiningTable]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiningTable](
	[DiningTableID] [int] IDENTITY(1,1) NOT NULL,
	[RestuarantID] [int] NOT NULL,
	[DiningLocation] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DiningTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiningTableTrack]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiningTableTrack](
	[DiningTableTrackID] [int] IDENTITY(1,1) NOT NULL,
	[DiningTableID] [int] NULL,
	[TableStatus] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[DiningTableTrackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantDetails]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantDetails](
	[RestuarantID] [int] IDENTITY(1,1) NOT NULL,
	[RestuarantName] [nvarchar](200) NOT NULL,
	[RestaurantAddress] [nvarchar](500) NOT NULL,
	[MobileNo] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RestuarantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[MobileNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RestuarantName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Cuisine]    Script Date: 12-11-2021 11:33:35 ******/
CREATE NONCLUSTERED INDEX [UK_Cuisine] ON [dbo].[Cuisine]
(
	[RestuarantID] ASC,
	[CuisineName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_DiningTable]    Script Date: 12-11-2021 11:33:35 ******/
CREATE NONCLUSTERED INDEX [UK_DiningTable] ON [dbo].[DiningTable]
(
	[RestuarantID] ASC,
	[DiningLocation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[OrderInfo] ([OrderID])
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD FOREIGN KEY([RestuarantID])
REFERENCES [dbo].[RestaurantDetails] ([RestuarantID])
GO
ALTER TABLE [dbo].[Cuisine]  WITH CHECK ADD FOREIGN KEY([RestuarantID])
REFERENCES [dbo].[RestaurantDetails] ([RestuarantID])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([RestuarantID])
REFERENCES [dbo].[RestaurantDetails] ([RestuarantID])
GO
ALTER TABLE [dbo].[DiningTable]  WITH CHECK ADD FOREIGN KEY([RestuarantID])
REFERENCES [dbo].[RestaurantDetails] ([RestuarantID])
GO
ALTER TABLE [dbo].[DiningTableTrack]  WITH CHECK ADD FOREIGN KEY([DiningTableID])
REFERENCES [dbo].[DiningTable] ([DiningTableID])
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD FOREIGN KEY([DiningTableID])
REFERENCES [dbo].[DiningTable] ([DiningTableID])
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD FOREIGN KEY([MenuItemID])
REFERENCES [dbo].[RestaurantMenuItem] ([MenuItemID])
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD FOREIGN KEY([RestuarantID])
REFERENCES [dbo].[RestaurantDetails] ([RestuarantID])
GO
ALTER TABLE [dbo].[RestaurantMenuItem]  WITH CHECK ADD FOREIGN KEY([CuisineID])
REFERENCES [dbo].[Cuisine] ([CuisineID])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [Chk_CustMobileNo] CHECK  (([MobileNo] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' AND len([MobileNo])=(10)))
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [Chk_CustMobileNo]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [Chk_CustomerName] CHECK  ((NOT [CustomerName] like '%[^A-Z ]%' AND len([CustomerName])>(10)))
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [Chk_CustomerName]
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [Chk_ItemQuantity] CHECK  (([ItemQuantity]>(0)))
GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [Chk_ItemQuantity]
GO
ALTER TABLE [dbo].[RestaurantDetails]  WITH CHECK ADD  CONSTRAINT [Chk_Address] CHECK  (([RestaurantAddress] like '[0-9]%' AND len([RestaurantAddress])>(10)))
GO
ALTER TABLE [dbo].[RestaurantDetails] CHECK CONSTRAINT [Chk_Address]
GO
ALTER TABLE [dbo].[RestaurantDetails]  WITH CHECK ADD  CONSTRAINT [Chk_MobileNo] CHECK  (([MobileNo] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' AND len([MobileNo])=(10)))
GO
ALTER TABLE [dbo].[RestaurantDetails] CHECK CONSTRAINT [Chk_MobileNo]
GO
ALTER TABLE [dbo].[RestaurantMenuItem]  WITH CHECK ADD  CONSTRAINT [chk_ItemPrice] CHECK  (([ItemPrice]>(0)))
GO
ALTER TABLE [dbo].[RestaurantMenuItem] CHECK CONSTRAINT [chk_ItemPrice]
GO
/****** Object:  StoredProcedure [dbo].[Bills_D]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[Bills_D]
(
@BillsID int
)
AS
BEGIN
	Delete from Bills where BillsID=@BillsID
END
GO
/****** Object:  StoredProcedure [dbo].[Bills_I]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[Bills_I]
(
@OrderID int,
@CustomerID int
)
AS
BEGIN
	DECLARE @BillAmount float
		set @BillAmount= (select OrderAmount from dbo.BillsTableFunction(@OrderID));
		--Print @BillAmount

		DECLARE @RestuarantID int 
		set @RestuarantID=(select RestuarantID from dbo.BillsTableFunction(@OrderID));
		--Print @RestaurantID

		IF EXISTS (Select 1 from Bills where (OrderID=@OrderID))
			BEGIN
				Print 'OrderID aready exists'
			END
		ELSE
			BEGIN
				INSERT INTO Bills
				(
					OrderID,
					RestuarantID,
					BillAmount,
					CustomerID
				)
				VALUES 
				(
					@OrderID,
					@RestuarantID,
					@BillAmount,
					@CustomerID
				)

				DECLARE @DiningTableID int
				UPDATE DiningTableTrack SET TableStatus='Vacant'
				where DiningTableID=(select DiningTableID 
				from OrderInfo where OrderID=@OrderID)
			END
END
GO
/****** Object:  StoredProcedure [dbo].[Cuisine_D]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[Cuisine_D]
(
 @CuisineID int,
 @RestuarantID int,
 @CuisineName Nvarchar(50)
)
AS
BEGIN
	DELETE FROM Cuisine where CuisineID=@CuisineID
END
GO
/****** Object:  StoredProcedure [dbo].[Cuisine_I]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--NOTES

--Query for inserting new data
-- exec Cuisine_I '20','Italian'

--Query for updating data
-- exec Cuisine_IU 8,'20','Italian'


-- Fetching data from table
--Select * from RestaurantDetails

--Select * from Cuisine

CREATE
 
 
PROCEDURE [dbo].[Cuisine_I]
(
 @RestuarantID int,
 @CuisineName Nvarchar(50)
)
AS
BEGIN
	INSERT INTO Cuisine
	(
	RestuarantID,
	CuisineName
	)
	VALUES
	(
	@RestuarantID,
	@CuisineName
	)	
END
GO
/****** Object:  StoredProcedure [dbo].[Cuisine_IU]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--NOTES

--Query for inserting new data
-- exec Cuisine_IU NULL,'19','Street Food'

--Query for updating data
-- exec Cuisine_IU 1,'19','Italian'


-- Fetching data from table
--Select * from RestaurantDetails

--Select * from Cuisine

CREATE
 
 
PROCEDURE [dbo].[Cuisine_IU]
(
 @CuisineID int NULL,
 @RestuarantID int,
 @CuisineName Nvarchar(50)
)
AS
BEGIN
	IF @CuisineID IS NULL
	BEGIN
		INSERT INTO Cuisine
		(
		RestuarantID,
		CuisineName
		)
		VALUES
		(
		@RestuarantID,
		@CuisineName
		)
	END
	ELSE
		BEGIN 
			UPDATE Cuisine SET
				RestuarantID=@RestuarantID, 
				CuisineName=@CuisineName 
				WHERE
					CuisineID=@CuisineID
				
		END
END
GO
/****** Object:  StoredProcedure [dbo].[Cuisine_U]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[Cuisine_U]
(
 @CuisineID int,
 @RestuarantID int,
 @CuisineName Nvarchar(50)
)
AS
BEGIN
	UPDATE Cuisine SET RestuarantID=@RestuarantID,CuisineName=@CuisineName 
	WHERE CuisineID=@CuisineID	
END
GO
/****** Object:  StoredProcedure [dbo].[Customer_D]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[Customer_D]
(
@CustomerID int,
@RestaurantID int,
@CustomerName Nvarchar(100),
@MobileNo Nvarchar(100)
)
AS
BEGIN
	DELETE FROM Customer where CustomerID=@CustomerID
END
GO
/****** Object:  StoredProcedure [dbo].[Customer_I]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Query for inserting data 
--exec Customer_I 20,'Ditya Poduval','7457568000'

--select * from RestaurantDetails
--select * from Customer
--Delete TOP(3) from Customer 

CREATE
 
 
PROCEDURE [dbo].[Customer_I]
(
@RestuarantID int,
@CustomerName Nvarchar(100), 
@MobileNo Nvarchar(20)
)
AS
BEGIN
	IF LEN(@MobileNo)=10
		BEGIN
			IF LEN(@CustomerName)>0
				BEGIN
					IF @CustomerName NOT LIKE '%[^A-Z ]%'
						BEGIN
							INSERT INTO Customer
							(
								RestuarantID,
								CustomerName,
								MobileNo
							)
							VALUES 
							(
								@RestuarantID,
								@CustomerName,
								@MobileNo
							)
						END
					ELSE
						BEGIN
							Print 'Customer name should not contain any special symbols'
						END
					END
			ELSE
				BEGIN
					Print 'Please enter Customers name with minimum 10 characters'
				END
		END
	ELSE
		BEGIN
			Print 'Please enter mobile number with length 10'
		END
END
GO
/****** Object:  StoredProcedure [dbo].[Customer_U]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec Customer_U 38,19,'Dhyana Nimmavat','9865327845'

--select * from Customer

CREATE
 
 
PROCEDURE [dbo].[Customer_U]
(
 @CustomerID int,
 @RestuarantID int,
 @CustomerName Nvarchar(100),
 @MobileNo Nvarchar(10)
)
AS
BEGIN
	IF LEN(@MobileNo)=10
		BEGIN
			IF LEN(@CustomerName)>0
				BEGIN
					IF @CustomerName NOT LIKE '%[^A-Z ]%'
						BEGIN

							UPDATE Customer SET 
								RestuarantID=@RestuarantID,
								CustomerName=@CustomerName,
								MobileNo=@MobileNo
							WHERE
								CustomerID=@CustomerID
						END
						
					ELSE
						BEGIN
							Print 'Customer name should not contain any special symbols'
						END
					END
			ELSE
				BEGIN
					Print 'Please enter Customers name with minimum 10 characters'
				END
		END
	ELSE
		BEGIN
			Print 'Please enter mobile number with length 10'
		END
END
GO
/****** Object:  StoredProcedure [dbo].[DiningTable_D]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[DiningTable_D]
(
@DiningTableID int
)
AS
BEGIN
	DELETE from DiningTable where DiningTableID=@DiningTableID
END
GO
/****** Object:  StoredProcedure [dbo].[DiningTable_I]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[DiningTable_I]
(
@RestuarantID int,
@DiningLocation Nvarchar(100)
)
AS
BEGIN
	BEGIN
		INSERT INTO DiningTable
		(
			RestuarantID,
			DiningLocation
		)
		VALUES 
		(
			@RestuarantID,
			@DiningLocation
		)
		END
END
GO
/****** Object:  StoredProcedure [dbo].[DiningTable_U]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[DiningTable_U]
(
@DiningTableID int,
@RestuarantID int,
@DiningLocation Nvarchar(100)
)
AS
BEGIN
	BEGIN
		UPDATE DiningTable SET RestuarantID=@RestuarantID,DiningLocation=@DiningLocation
		where DiningTableID=@DiningTableID

	END
END
GO
/****** Object:  StoredProcedure [dbo].[DiningTableTrack_I]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Query for inserting data 
--exec DiningTableTrack_I 2,'Occupied'

--select * from DiningTable
--select * from DiningTableTrack


CREATE
 
 
PROCEDURE [dbo].[DiningTableTrack_I]
(
@DiningTableID int,
@TableStatus Nvarchar(200)
)
AS
BEGIN
		INSERT INTO DiningTableTrack
		(
			DiningTableID,
			TableStatus
		)
		VALUES 
		(
			@DiningTableID,
			@TableStatus
		)
END
GO
/****** Object:  StoredProcedure [dbo].[OrderInfo_D]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec OrderInfo_D 43,'2021-10-21',19,42,4,100,3


--Fetcting data from table
--select * from RestaurantDetails
--select * from RestaurantMenuItem
--select * from Cuisine
--select * from DiningTable
-- 
--select * from DiningTableTrack
--DELETE TOP(1) from OrderInfo
--select * from Bills
--DELETE TOP(4) from Bills



CREATE
 
 
PROCEDURE [dbo].[OrderInfo_D]
(
@OrderID int,
@OrderDate Datetime,
@RestuarantID int,
@MenuItemID int,
@ItemQuantity int,
@OrderAmount float,
@DiningTableID int
)
AS
BEGIN

	
	IF @OrderDate=convert(date, GETDATE())
	BEGIN

	set @OrderAmount=@ItemQuantity*dbo.CalculateItemPrice(@MenuItemID)
	
	Delete from OrderInfo where OrderID=@OrderID;
	END
		ELSE
			BEGIN
				Print 'Note : Please enter todays date'
			END
	
END
GO
/****** Object:  StoredProcedure [dbo].[OrderInfo_I]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[OrderInfo_I]
(
@OrderDate Datetime,
@RestuarantID int,
@MenuItemID int,
@ItemQuantity int,
@OrderAmount float,
@DiningTableID int
)
AS
BEGIN

	DECLARE @OrderID int
	IF @OrderDate=convert(date, GETDATE())
	BEGIN	
		
		--TableStatus

		DECLARE @TableStatus Nvarchar(200)
		select @TableStatus=TableStatus from DiningTableTrack where DiningTableID=@DiningTableID
		--return @TableStatus

		IF (@TableStatus='Occupied')
			BEGIN
				Print 'Table is Occupied'
			END
		ELSE
			BEGIN
				update DiningTableTrack set	TableStatus='Occupied'
				where DiningTableID=@DiningTableID

				--DECLARE @OrderAmount float
				set @OrderAmount=@ItemQuantity*dbo.CalculateItemPrice(@MenuItemID)
		
				set @OrderID=(Select @OrderID from OrderInfo 
								where DiningTableID=@DiningTableID and RestuarantID=@RestuarantID and MenuItemID=@MenuItemID)	
				Print @OrderID
		
				INSERT INTO OrderInfo
				(
					OrderDate,
					RestuarantID,
					MenuItemID,
					ItemQuantity,
					OrderAmount,
					DiningTableID
				)
				VALUES 
				(
					@OrderDate,
					@RestuarantID,
					@MenuItemID,
					@ItemQuantity,
					@OrderAmount,
					@DiningTableID
				)

						--Copied insert query code
			END
		END
	ELSE
		BEGIN
			Print 'Note : Please enter todays date'
		END
	
END
GO
/****** Object:  StoredProcedure [dbo].[OrderInfo_U]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec OrderInfo_U 43,'2021-10-21',19,42,4,100,3


--Fetcting data from table
--select * from RestaurantDetails
--select * from RestaurantMenuItem
--select * from Cuisine
--select * from DiningTable
-- 
--select * from DiningTableTrack
--DELETE TOP(1) from OrderInfo
--select * from Bills
--DELETE TOP(4) from Bills



CREATE
 
 
PROCEDURE [dbo].[OrderInfo_U]
(
@OrderID int,
@OrderDate Datetime,
@RestuarantID int,
@MenuItemID int,
@ItemQuantity int,
@OrderAmount float,
@DiningTableID int
)
AS
BEGIN

	
	IF @OrderDate=convert(date, GETDATE())
	BEGIN

	set @OrderAmount=@ItemQuantity*dbo.CalculateItemPrice(@MenuItemID)
			UPDATE OrderInfo SET

			OrderDate=@OrderDate,
			RestuarantID=@RestuarantID,
			MenuItemID=@MenuItemID,
			ItemQuantity=@ItemQuantity,
			OrderAmount=@OrderAmount,
			DiningTableID=@DiningTableID
			where OrderID=@OrderID
		
		END
		ELSE
			BEGIN
				Print 'Note : Please enter todays date'
			END
	
END
GO
/****** Object:  StoredProcedure [dbo].[RestaurantDetails_D]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[RestaurantDetails_D]
(--Mentioning all the parameteres as mentioned in the assignment that query should take input for name, address and mobile number
@RestuarantID int,
@RestaurantName Nvarchar(200), 
@RestaurantAddress Nvarchar(500), 
@MobileNo Nvarchar(10)
)
AS
BEGIN TRY
IF EXISTS(select 1 from RestaurantDetails where RestuarantID=@RestuarantID)
	BEGIN 
		DELETE FROM RestaurantDetails where RestuarantID=@RestuarantID
	END
	ELSE
		BEGIN
			RAISERROR ('Restaurant ID does not exist',16,1)
		END
END TRY
BEGIN CATCH
	

	DECLARE @ERRORMEG NVARCHAR(512)
		SELECT @ERRORMEG=ERROR_MESSAGE()
		ROLLBACK TRAN
		RAISERROR(@ERRORMEG,16,1);
		SELECT 'CATCH BLOCK'
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[RestaurantDetails_I]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec RestaurantDetails_I 'demo13','1-Block E, PalmGreens, Reliance Green', '9695747475'
--exec RestaurantDetails_I 'MIRCH MASALA','21-Sec Reliance Greens', '999888777888'

--Select * from RestaurantDetails

CREATE
 
 
PROCEDURE [dbo].[RestaurantDetails_I]
(
@RestaurantName Nvarchar(200), 
@RestaurantAddress Nvarchar(500), 
@MobileNo Nvarchar(20)
)
AS
BEGIN TRY
	IF NOT EXISTS(select 1 from RestaurantDetails where RestuarantName=@RestaurantName and MobileNo=@MobileNo)
	BEGIN
		IF LEN(@MobileNo)=10
		BEGIN
			IF LEN(@RestaurantAddress)>10
			BEGIN
				IF @RestaurantAddress LIKE '[0-9]%'
				BEGIN
					INSERT INTO RestaurantDetails
					(
						RestuarantName, 
						RestaurantAddress, 
						MobileNo)
					VALUES (
							@RestaurantName,
							@RestaurantAddress, 
							@MobileNo)
			
				END
				ELSE
					BEGIN
						RAISERROR (' Address should contain number as 1st character',16,1)
					END
				END
			ELSE
				BEGIN
					RAISERROR ('Please enter address whose length is more than 10 digits',16,1)
				END
			END
		ELSE
			BEGIN
				RAISERROR ('Please enter the mobile no of 10 digits',16,1)
			END
	END
	ELSE
	BEGIN
		RAISERROR ('Data already exists',16,1)
	END
END TRY
BEGIN CATCH
--	--SELECT ERROR_MESSAGE() as ErrorMessage;

	DECLARE @ERRORMEG NVARCHAR(512)
		SELECT @ERRORMEG=ERROR_MESSAGE()
		ROLLBACK TRAN
		RAISERROR(@ERRORMEG,16,1);
		SELECT 'CATCH BLOCK'
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[RestaurantDetails_U]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[RestaurantDetails_U]
(
@RestuarantID int,
@RestaurantName Nvarchar(200), 
@RestaurantAddress Nvarchar(500), 
@MobileNo Nvarchar(10)
)
AS
BEGIN TRY
	IF LEN(@MobileNo)=10
		BEGIN
			IF LEN(@RestaurantAddress)>10
				BEGIN
					IF @RestaurantAddress LIKE '[0-9]%'
						BEGIN
		
							UPDATE RestaurantDetails SET 
								RestuarantName=@RestaurantName, 
								RestaurantAddress=@RestaurantAddress, 
								MobileNo=@MobileNo
							WHERE
								RestuarantID=@RestuarantID
						 END
					ELSE
						BEGIN
							RAISERROR (' Address should contain number as 1st character',16,1)
						END
					END
			ELSE
				BEGIN
					RAISERROR ('Please enter address whose length is more than 10 digits',16,1)
				END
			END
		ELSE
			BEGIN
				RAISERROR ('Please enter the mobile no of 10 digits',16,1)
			END
END TRY
BEGIN CATCH
	--SELECT ERROR_MESSAGE() as ErrorMessage;

	DECLARE @ERRORMEG NVARCHAR(512)
		SELECT @ERRORMEG=ERROR_MESSAGE()
		ROLLBACK TRAN
		RAISERROR(@ERRORMEG,16,1);
		SELECT 'CATCH BLOCK'
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[RestaurantMenuItem_D]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[RestaurantMenuItem_D]
(
@MenuItemID int,
@CuisineID int, 
@ItemName Nvarchar(100), 
@ItemPrice float
)
AS
BEGIN
	DELETE FROM RestaurantMenuItem where MenuItemID=@MenuItemID
END
GO
/****** Object:  StoredProcedure [dbo].[RestaurantMenuItem_I]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec RestaurantMenuItem_I 11,'Cheese Paneer Dosa',350


--Fetcting data from table
--select * from RestaurantMenuItem
--select * from Cuisine

CREATE
 
 
PROCEDURE [dbo].[RestaurantMenuItem_I]
(
@CuisineID int,
@ItemName Nvarchar(100), 
@ItemPrice float
)
AS
BEGIN
	IF(@ItemPrice>0)
		BEGIN

			INSERT INTO RestaurantMenuItem
			(
				CuisineID,
				ItemName,
				ItemPrice
			)
			VALUES 
			(
				@CuisineID,
				@ItemName, 
				@ItemPrice
			)
		END
	ELSE
		BEGIN
			Print 'Item Price should be greater than 0'
		END
END
GO
/****** Object:  StoredProcedure [dbo].[RestaurantMenuItem_U]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec RestaurantMenuItem_U 33,1,'Malai Kofta',275

--select * from RestaurantMenuItem

CREATE
 
 
PROCEDURE [dbo].[RestaurantMenuItem_U]
(
@MenuItemID int,
@CuisineID int, 
@ItemName Nvarchar(100), 
@ItemPrice float
)
AS
BEGIN
	IF(@ItemPrice>0)
		BEGIN
			UPDATE RestaurantMenuItem SET 
				CuisineID=@CuisineID,
				ItemName=@ItemName,
				ItemPrice=@ItemPrice
			WHERE
				MenuItemID=@MenuItemID
		END
	ELSE
		BEGIN
			Print 'Item Price should be greater than 0'
		END
END
GO
/****** Object:  StoredProcedure [dbo].[UFDTableStatus]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 
 
PROCEDURE [dbo].[UFDTableStatus]
(
@RestuarantID int
)
AS
BEGIN	
	IF @RestuarantID IS NULL
		BEGIN
			Select RD.RestuarantName,DT.DiningTableID,DT.DiningLocation, DTT.TableStatus,DT.RestuarantID
			From RestaurantDetails RD
			INNER JOIN DiningTable DT 
				on RD.RestuarantID=DT.RestuarantID
			INNER JOIN DiningTableTrack DTT
				on DT.DiningTableID=DTT.DiningTableID
			where TableStatus='Vacant'
		END
	ELSE
		BEGIN
			Select RD.RestuarantName,DT.DiningTableID,DT.DiningLocation, DTT.TableStatus,DT.RestuarantID
			From RestaurantDetails RD
			INNER JOIN DiningTable DT 
				on RD.RestuarantID=DT.RestuarantID
			INNER JOIN DiningTableTrack DTT
				on DT.DiningTableID=DTT.DiningTableID
			Where DT.RestuarantID=@RestuarantID and TableStatus='Vacant'
		END
END
GO
/****** Object:  StoredProcedure [dbo].[USPDayWiseAmount]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 

PROCEDURE [dbo].[USPDayWiseAmount]
(
	@RestuarantID int
)
AS
BEGIN
	IF @RestuarantID IS NULL
		BEGIN
			select R.RestuarantName,YEAR(O.OrderDate) As Year, 
					MONTH(O.OrderDate) As Month, DAY(O.OrderDate) as Day, 
					DATENAME(dw,O.OrderDate) as DayName,SUM(O.OrderAmount) As TotalAmount 
			from OrderInfo O
			inner join RestaurantDetails R on R.RestuarantID=O.RestuarantID
			--inner join DiningTable DT on O.DiningTableID=DT.DiningTableID
			Group By Day(O.OrderDate),YEAR(O.OrderDate),MONTH(O.OrderDate),DATENAME(DW,O.OrderDate),R.RestuarantName--,DT.DiningLocation
		END
	ELSE
		BEGIN
			select R.RestuarantName,YEAR(O.OrderDate) As Year, 
					MONTH(O.OrderDate) As Month, DAY(O.OrderDate) as Day, 
					DATENAME(dw,O.OrderDate) as DayName,SUM(O.OrderAmount) As TotalAmount 
			from OrderInfo O
			inner join RestaurantDetails R on R.RestuarantID=@RestuarantID and O.RestuarantID=@RestuarantID
			--inner join DiningTable DT on O.DiningTableID=DT.DiningTableID and DT.RestuarantID=19
			Group By Day(O.OrderDate),YEAR(O.OrderDate),MONTH(O.OrderDate),DATENAME(DW,O.OrderDate),R.RestuarantName--,DT.DiningLocation
		END
END
GO
/****** Object:  StoredProcedure [dbo].[USPDisplayCustomerDetails]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec USPDisplayCustomerDetails 'where O.RestuarantID=20','Order By C.CustomerName Asc'
--exec USPDisplayCustomerDetails null,'Order By C.CustomerName Asc'
--exec USPDisplayCustomerDetails null,null

CREATE
 

PROCEDURE [dbo].[USPDisplayCustomerDetails]
(
	@FilterBy Nvarchar(1000),
	@OrderBy Nvarchar(1000)
)
AS
BEGIN
	DECLARE @SQL Nvarchar(max)=''
	IF (@FilterBy IS NULL) AND (@OrderBy IS NULL) 
		BEGIN
			
			set @SQL='select C.CustomerName, C.MobileNo,O.OrderID, O.OrderDate, O.OrderAmount, CU.CuisineName, RM.ItemName, RM.ItemPrice,DT.DiningLocation 
			from  Bills B 
			Inner Join OrderInfo O on O.OrderID=B.OrderID
			Inner Join RestaurantMenuItem RM on RM.MenuItemID=O.MenuItemID
			Inner Join Cuisine CU on CU.CuisineID=RM.CuisineID 
			Inner Join DiningTable DT on DT.DiningTableID=O.DiningTableID
			and DT.RestuarantID=O.RestuarantID
			Inner Join Customer C on C.CustomerID=B.CustomerID' --+' '+@FilterBy+' '+@OrderBy

			exec (@SQL)
		END
	else IF (@OrderBy IS NULL) OR (@FilterBy IS NULL)
		BEGIN
			IF(@OrderBy IS NOT NULL) AND (@FilterBy IS NULL)
				BEGIN
					set @SQL='select C.CustomerName, C.MobileNo,O.OrderID, O.OrderDate, O.OrderAmount, CU.CuisineName, RM.ItemName, RM.ItemPrice,DT.DiningLocation 
					from  Bills B 
					Inner Join OrderInfo O on O.OrderID=B.OrderID
					Inner Join RestaurantMenuItem RM on RM.MenuItemID=O.MenuItemID
					Inner Join Cuisine CU on CU.CuisineID=RM.CuisineID 
					Inner Join DiningTable DT on DT.DiningTableID=O.DiningTableID
					and DT.RestuarantID=O.RestuarantID
					Inner Join Customer C on C.CustomerID=B.CustomerID' +' '+@OrderBy --+' '+@OrderBy

					exec (@SQL)
				END
			ELSE IF(@OrderBy IS NULL) AND (@FilterBy IS NOT NULL)
				BEGIN
					set @SQL='select C.CustomerName, C.MobileNo,O.OrderID, O.OrderDate, O.OrderAmount, CU.CuisineName, RM.ItemName, RM.ItemPrice,DT.DiningLocation 
					from  Bills B 
					Inner Join OrderInfo O on O.OrderID=B.OrderID
					Inner Join RestaurantMenuItem RM on RM.MenuItemID=O.MenuItemID
					Inner Join Cuisine CU on CU.CuisineID=RM.CuisineID 
					Inner Join DiningTable DT on DT.DiningTableID=O.DiningTableID
					and DT.RestuarantID=O.RestuarantID
					Inner Join Customer C on C.CustomerID=B.CustomerID' +' '+@FilterBy --+' '+@OrderBy

					exec (@SQL)
				END
		END
	ELSE
		BEGIN
			set @SQL='select C.CustomerName, C.MobileNo,O.OrderID, O.OrderDate, O.OrderAmount, CU.CuisineName, RM.ItemName, RM.ItemPrice,DT.DiningLocation 
			from  Bills B 
			Inner Join OrderInfo O on O.OrderID=B.OrderID
			Inner Join RestaurantMenuItem RM on RM.MenuItemID=O.MenuItemID
			Inner Join Cuisine CU on CU.CuisineID=RM.CuisineID 
			Inner Join DiningTable DT on DT.DiningTableID=O.DiningTableID
			and DT.RestuarantID=O.RestuarantID
			Inner Join Customer C on C.CustomerID=B.CustomerID' +' '+@FilterBy+' '+@OrderBy

			exec (@SQL)
		END
END

--select * from OrderInfo
--select * From Bills
--select * from DiningTableTrack
--select * from DiningTable
--select * from Customer
--select * from Cuisine
--select * from RestaurantMenuItem
--select * from RestaurantDetails
GO
/****** Object:  StoredProcedure [dbo].[USPYearTotalAmount]    Script Date: 12-11-2021 11:33:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE
 

PROCEDURE [dbo].[USPYearTotalAmount]
(
	@RestuarantID int
)
AS
BEGIN
		IF @RestuarantID IS NULL
			BEGIN
				select R.RestuarantName,SUM(O.OrderAmount) As TotalAmount, YEAR(O.OrderDate) as Year
				from RestaurantDetails R Inner join OrderInfo O
				on R.RestuarantID=O.RestuarantID
				GROUP BY YEAR(O.OrderDate), R.RestuarantName
			END	
		ELSE
			BEGIN
				select R.RestuarantName,SUM(O.OrderAmount) As TotalAmount, YEAR(O.OrderDate) as Year
				from RestaurantDetails R Inner join OrderInfo O
				on R.RestuarantID=O.RestuarantID
				where O.RestuarantID = @RestuarantID
				GROUP BY YEAR(O.OrderDate), R.RestuarantName
			END	
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "R"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_CuisineDetails'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vw_CuisineDetails'
GO
USE [master]
GO
ALTER DATABASE [Restaurant] SET  READ_WRITE 
GO
