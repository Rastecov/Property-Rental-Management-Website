USE [master]
GO
/****** Object:  Database [PropertyRentalDB]    Script Date: 12/5/2022 4:45:52 PM ******/
CREATE DATABASE [PropertyRentalDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PropertyRentalDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PropertyRentalDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PropertyRentalDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PropertyRentalDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PropertyRentalDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PropertyRentalDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PropertyRentalDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PropertyRentalDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PropertyRentalDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PropertyRentalDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PropertyRentalDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PropertyRentalDB] SET  MULTI_USER 
GO
ALTER DATABASE [PropertyRentalDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PropertyRentalDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PropertyRentalDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PropertyRentalDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PropertyRentalDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PropertyRentalDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PropertyRentalDB] SET QUERY_STORE = OFF
GO
USE [PropertyRentalDB]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 12/5/2022 4:45:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressId] [nchar](10) NOT NULL,
	[StreetNumber] [varchar](50) NULL,
	[StreetName] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Province] [varchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Apartments]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apartments](
	[ApartmentNumber] [nchar](10) NOT NULL,
	[ApartmentType] [varchar](50) NULL,
	[Description] [varchar](50) NULL,
	[Floor] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[BuildingId] [nchar](10) NULL,
 CONSTRAINT [PK_Apartments] PRIMARY KEY CLUSTERED 
(
	[ApartmentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppointmentId] [nchar](10) NOT NULL,
	[AppointmentDate] [date] NULL,
	[AppointmentTime] [time](7) NULL,
	[TenantId] [nchar](10) NULL,
	[EmployeeId] [nchar](10) NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Buildings]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buildings](
	[BuildingId] [nchar](10) NOT NULL,
	[BuildingName] [varchar](50) NULL,
	[AdressId] [nchar](10) NULL,
 CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [nchar](10) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[PhoneNumber] [varchar](50) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [nchar](10) NOT NULL,
	[Message] [varchar](50) NULL,
	[TenantId] [nchar](10) NOT NULL,
	[EmployeeId] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[UserId] [nchar](10) NOT NULL,
	[BuildingId] [nchar](10) NOT NULL,
	[AdressId] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rentals]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rentals](
	[RentalId] [nchar](10) NOT NULL,
	[RentalPrice] [nchar](10) NULL,
	[StartDate] [date] NULL,
	[Enddate] [date] NULL,
	[ApartmentId] [nchar](10) NOT NULL,
	[TenantId] [nchar](10) NOT NULL,
	[EmployeeId] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Rentals_1] PRIMARY KEY CLUSTERED 
(
	[RentalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[ScheduleId] [nchar](10) NOT NULL,
	[ScheduleDate] [date] NULL,
	[ScheduleTime] [time](7) NULL,
	[TenantId] [nchar](10) NULL,
	[EmployeeId] [nchar](10) NULL,
	[AppointmentId] [nchar](10) NULL,
 CONSTRAINT [PK_Schedules_1] PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenants1]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenants1](
	[TenantId] [nchar](10) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[PhoneNumber] [varchar](50) NULL,
 CONSTRAINT [PK_Tenants1_1] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users1]    Script Date: 12/5/2022 4:45:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users1](
	[UserId] [nchar](10) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[UserType] [varchar](50) NULL,
 CONSTRAINT [PK_Users1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Addresses] ([AddressId], [StreetNumber], [StreetName], [City], [Province], [PostalCode]) VALUES (N'0001      ', N'2000', N'Rue Sainte Catherine O', N'Montreal', N'QC', N'H2T 3K4')
GO
INSERT [dbo].[Apartments] ([ApartmentNumber], [ApartmentType], [Description], [Floor], [Status], [BuildingId]) VALUES (N'1111      ', N'Condo', N'2 bed 1 bathroom', N'2', N'Available ', N'1234      ')
GO
INSERT [dbo].[Appointments] ([AppointmentId], [AppointmentDate], [AppointmentTime], [TenantId], [EmployeeId]) VALUES (N'0001      ', CAST(N'2010-12-05' AS Date), CAST(N'05:00:00' AS Time), N'2345      ', N'1234      ')
GO
INSERT [dbo].[Buildings] ([BuildingId], [BuildingName], [AdressId]) VALUES (N'1234      ', N'Complexe Sociale', N'0001      ')
GO
INSERT [dbo].[Employees] ([EmployeeId], [FirstName], [LastName], [Email], [PhoneNumber]) VALUES (N'1234      ', N'Marc', N'Bellefeuille', N'Marc@hotmail.com', N'4388766543')
GO
INSERT [dbo].[Messages] ([MessageId], [Message], [TenantId], [EmployeeId]) VALUES (N'2000      ', N'It was a good meting!!! Thank you for your help.', N'2345      ', N'1234      ')
GO
INSERT [dbo].[Properties] ([UserId], [BuildingId], [AdressId]) VALUES (N'2345      ', N'1234      ', N'0001      ')
GO
INSERT [dbo].[Rentals] ([RentalId], [RentalPrice], [StartDate], [Enddate], [ApartmentId], [TenantId], [EmployeeId]) VALUES (N'1010      ', N'1000$     ', CAST(N'2022-12-20' AS Date), CAST(N'2023-12-20' AS Date), N'1111      ', N'2345      ', N'1234      ')
GO
INSERT [dbo].[Schedules] ([ScheduleId], [ScheduleDate], [ScheduleTime], [TenantId], [EmployeeId], [AppointmentId]) VALUES (N'1000      ', CAST(N'2022-12-15' AS Date), CAST(N'05:00:00' AS Time), N'2345      ', N'1234      ', N'0001      ')
GO
INSERT [dbo].[Tenants1] ([TenantId], [FirstName], [LastName], [Email], [PhoneNumber]) VALUES (N'2345      ', N'Jules', N'Salazar', N'Jules@yahoo.com', N'43876542154')
INSERT [dbo].[Tenants1] ([TenantId], [FirstName], [LastName], [Email], [PhoneNumber]) VALUES (N'6789      ', N'Rajdeep', N'Alaam', N'Raj@hotmail.com', N'4385766523')
GO
INSERT [dbo].[Users1] ([UserId], [Username], [Password], [UserType]) VALUES (N'0001      ', N'Undefeated ', N'qwer1234', N'Employee')
INSERT [dbo].[Users1] ([UserId], [Username], [Password], [UserType]) VALUES (N'1234      ', N'Rastcov', N'qwer1234', N'Employee')
INSERT [dbo].[Users1] ([UserId], [Username], [Password], [UserType]) VALUES (N'2345      ', N'money ', N'qwer1234', N'Tenants')
INSERT [dbo].[Users1] ([UserId], [Username], [Password], [UserType]) VALUES (N'6789      ', N'Boss', N'qwer1234', N'Tenants')
GO
ALTER TABLE [dbo].[Apartments]  WITH CHECK ADD  CONSTRAINT [FK_Apartments_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[Apartments] CHECK CONSTRAINT [FK_Apartments_Buildings]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Employees]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Tenants1] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants1] ([TenantId])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Tenants1]
GO
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Addresses1] FOREIGN KEY([AdressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Addresses1]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Users1] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Users1] ([UserId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Users1]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Employees]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Tenants1] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants1] ([TenantId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Tenants1]
GO
ALTER TABLE [dbo].[Properties]  WITH CHECK ADD  CONSTRAINT [FK_Properties_Addresses] FOREIGN KEY([AdressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
ALTER TABLE [dbo].[Properties] CHECK CONSTRAINT [FK_Properties_Addresses]
GO
ALTER TABLE [dbo].[Properties]  WITH CHECK ADD  CONSTRAINT [FK_Properties_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[Properties] CHECK CONSTRAINT [FK_Properties_Buildings]
GO
ALTER TABLE [dbo].[Rentals]  WITH CHECK ADD  CONSTRAINT [FK_Rentals_Apartments1] FOREIGN KEY([ApartmentId])
REFERENCES [dbo].[Apartments] ([ApartmentNumber])
GO
ALTER TABLE [dbo].[Rentals] CHECK CONSTRAINT [FK_Rentals_Apartments1]
GO
ALTER TABLE [dbo].[Rentals]  WITH CHECK ADD  CONSTRAINT [FK_Rentals_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Rentals] CHECK CONSTRAINT [FK_Rentals_Employees]
GO
ALTER TABLE [dbo].[Rentals]  WITH CHECK ADD  CONSTRAINT [FK_Rentals_Tenants1] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants1] ([TenantId])
GO
ALTER TABLE [dbo].[Rentals] CHECK CONSTRAINT [FK_Rentals_Tenants1]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Appointments1] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([AppointmentId])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Appointments1]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Employees]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Tenants1] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants1] ([TenantId])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Tenants1]
GO
ALTER TABLE [dbo].[Tenants1]  WITH CHECK ADD  CONSTRAINT [FK_Tenants1_Users1] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Users1] ([UserId])
GO
ALTER TABLE [dbo].[Tenants1] CHECK CONSTRAINT [FK_Tenants1_Users1]
GO
USE [master]
GO
ALTER DATABASE [PropertyRentalDB] SET  READ_WRITE 
GO
