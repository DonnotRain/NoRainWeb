USE [master]
GO
/****** Object:  Database [BusinessRights]    Script Date: 2014/7/25 1:16:37 ******/
CREATE DATABASE [BusinessRights]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HWRights', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BusinessRights.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HWRights_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BusinessRights_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BusinessRights] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BusinessRights].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BusinessRights] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BusinessRights] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BusinessRights] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BusinessRights] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BusinessRights] SET ARITHABORT OFF 
GO
ALTER DATABASE [BusinessRights] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BusinessRights] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BusinessRights] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BusinessRights] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BusinessRights] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BusinessRights] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BusinessRights] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BusinessRights] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BusinessRights] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BusinessRights] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BusinessRights] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BusinessRights] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BusinessRights] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BusinessRights] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BusinessRights] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BusinessRights] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BusinessRights] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BusinessRights] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BusinessRights] SET RECOVERY FULL 
GO
ALTER DATABASE [BusinessRights] SET  MULTI_USER 
GO
ALTER DATABASE [BusinessRights] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BusinessRights] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BusinessRights] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BusinessRights] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BusinessRights', N'ON'
GO
USE [BusinessRights]
GO
/****** Object:  UserDefinedFunction [dbo].[getTopOUID]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[getTopOUID](@OUID int) returns int
as
 begin
  declare @root int
  set @root=(select id from ou where PID=-1)
  while (select pid from ou where id=@OUID )!=@root and (select pid from ou where id=@OUID )!=-1
  set @OUID=(select pid from ou where id=@OUID )
  return @OUID
 end




GO
/****** Object:  Table [dbo].[Functions]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Functions](
	[ID] [int] NOT NULL,
	[PID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ControlID] [nvarchar](100) NOT NULL,
	[SystemType_ID] [nvarchar](12) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[FunctionType] [int] NULL,
	[Sort] [int] NULL,
	[ImageIndex] [nvarchar](100) NULL,
	[IsEnabled] [bit] NULL,
	[IsInMenu] [bit] NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OU]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OU](
	[ID] [int] NOT NULL,
	[PID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Note] [ntext] NOT NULL,
	[Position] [bit] NOT NULL,
	[OUOrder] [int] NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OU_Role]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OU_Role](
	[OU_ID] [int] NOT NULL,
	[Role_ID] [int] NOT NULL,
	[TopOUID] [int] NOT NULL,
 CONSTRAINT [PK_Group_Role] PRIMARY KEY CLUSTERED 
(
	[OU_ID] ASC,
	[Role_ID] ASC,
	[TopOUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OU_User]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OU_User](
	[User_ID] [int] NOT NULL,
	[OU_ID] [int] NOT NULL,
 CONSTRAINT [PK_Group_User] PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC,
	[OU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Note] [ntext] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role_Function]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role_Function](
	[Role_ID] [int] NOT NULL,
	[Function_ID] [int] NOT NULL,
 CONSTRAINT [PK_Role_Function] PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC,
	[Function_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemAuthorize]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemAuthorize](
	[ID] [int] NOT NULL,
	[SystemType_OID] [nvarchar](12) NOT NULL,
	[Content] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SystemAuthorize] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemType]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemType](
	[OID] [nvarchar](12) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CustomID] [nvarchar](50) NOT NULL,
	[Authorize] [nvarchar](100) NOT NULL,
	[Note] [ntext] NOT NULL,
 CONSTRAINT [PK_SystemType] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Role]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Role](
	[User_ID] [int] NOT NULL,
	[Role_ID] [int] NOT NULL,
	[TopOUID] [int] NOT NULL,
 CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC,
	[Role_ID] ASC,
	[TopOUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2014/7/25 1:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] NOT NULL,
	[PID] [int] NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[IsExpire] [bit] NULL,
	[Title] [nvarchar](100) NULL,
	[IdentityCard] [nvarchar](50) NULL,
	[MobilePhone] [nvarchar](100) NULL,
	[OfficePhone] [nvarchar](100) NULL,
	[HomePhone] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Address] [nvarchar](200) NULL,
	[CustomField] [ntext] NULL,
	[TopOUID] [int] NOT NULL,
	[Remark] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[Functions] ([ID], [PID], [Name], [ControlID], [SystemType_ID], [Path], [FunctionType], [Sort], [ImageIndex], [IsEnabled], [IsInMenu]) VALUES (1000, -1, N'商品基础信息', N'Product', N'IPSPSD', N'', 1, 1, N'icon-th       ', NULL, NULL)
INSERT [dbo].[Functions] ([ID], [PID], [Name], [ControlID], [SystemType_ID], [Path], [FunctionType], [Sort], [ImageIndex], [IsEnabled], [IsInMenu]) VALUES (1001, -1, N'系统基础信息', N'SysParam', N'IPSPSD', N'', 1, 100, N'icon-cog       ', NULL, NULL)
INSERT [dbo].[Functions] ([ID], [PID], [Name], [ControlID], [SystemType_ID], [Path], [FunctionType], [Sort], [ImageIndex], [IsEnabled], [IsInMenu]) VALUES (1002, -1, N'订单信息', N'Order', N'IPSPSD', N'', 1, 20, N'icon-th-large       ', NULL, NULL)
INSERT [dbo].[Functions] ([ID], [PID], [Name], [ControlID], [SystemType_ID], [Path], [FunctionType], [Sort], [ImageIndex], [IsEnabled], [IsInMenu]) VALUES (1000001, 1000, N'商品管理', N'ProductIndex', N'IPSPSD', N'/Back/Product/index', 1, 1, N'icon-trophy       ', NULL, NULL)
INSERT [dbo].[Functions] ([ID], [PID], [Name], [ControlID], [SystemType_ID], [Path], [FunctionType], [Sort], [ImageIndex], [IsEnabled], [IsInMenu]) VALUES (1000002, 1000, N'商品单位管理', N'ProductUnit', N'IPSPSD', N'/back/ProductUnit/Index', 1, 2, N'icon-bookmark-empty       ', NULL, NULL)
INSERT [dbo].[Functions] ([ID], [PID], [Name], [ControlID], [SystemType_ID], [Path], [FunctionType], [Sort], [ImageIndex], [IsEnabled], [IsInMenu]) VALUES (1001001, 1001, N'分类项管理', N'Category', N'IPSPSD', N'/Back/Category/Index', 1, 1, N'icon-th-list       ', NULL, NULL)
INSERT [dbo].[Functions] ([ID], [PID], [Name], [ControlID], [SystemType_ID], [Path], [FunctionType], [Sort], [ImageIndex], [IsEnabled], [IsInMenu]) VALUES (1001002, 1001, N'系统功能管理', N'Function', N'IPSPSD', N'/Back/Plug/Index', 1, 3, N'icon-cogs       ', NULL, NULL)
INSERT [dbo].[Functions] ([ID], [PID], [Name], [ControlID], [SystemType_ID], [Path], [FunctionType], [Sort], [ImageIndex], [IsEnabled], [IsInMenu]) VALUES (1002001, 1002, N'未配送订单', N'OrdersNotFinished', N'IPSPSD', N'/Back/Order/Index', 1, 1, N'icon-star       ', NULL, NULL)
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (1, -1, N'广铁集团', N'', N'', 0, -1, N'1.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (5, 24, N'棠溪检修车间', N'', N'', 0, 9, N'1.24.5.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (6, 5, N'广州北接触网工区', N'', N'', 0, 1, N'1.24.5.6.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (7, 5, N'棠溪接触网工区', N'', N'', 0, 2, N'1.24.5.7.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (24, 1, N'广州供电段', N'', N'', 0, 3, N'1.24.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (25, 1, N'长沙供电段', N'', N'', 0, 4, N'1.25.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (26, 1, N'怀化供电段', N'', N'', 0, 5, N'1.26.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (27, 1, N'深圳供电段', N'', N'', 0, 6, N'1.27.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (28, 1, N'粤海公司', N'', N'', 0, 7, N'1.28.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (29, 24, N'技术科', N'', N'', 0, 4, N'1.24.29.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (30, 24, N'高铁技术科11', N'', N'', 0, 5, N'1.24.30.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (31, 24, N'安全生产调度指挥中心', N'', N'', 0, 7, N'1.24.31.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (32, 24, N'段领导', N'', N'', 0, 2, N'1.24.32.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (33, 24, N'计算机信息中心', N'', N'', 0, 6, N'1.24.33.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (34, 24, N'乐昌供电车间', N'', N'', 0, 10, N'1.24.34.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (35, 24, N'韶关东检修车间', N'', N'', 0, 11, N'1.24.35.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (36, 24, N'英德供电车间', N'', N'', 0, 12, N'1.24.36.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (37, 34, N'坪石接触网工区', N'', N'', 0, 1, N'1.24.34.37.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (38, 34, N'乐昌接触网工区', N'', N'', 0, 2, N'1.24.34.38.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (39, 34, N'大瑶山接触网巡守工区', N'', N'', 0, 3, N'1.24.34.39.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (40, 35, N'韶关东接触网工区', N'', N'', 0, 1, N'1.24.35.40.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (41, 35, N'沙口接触网工区', N'', N'', 0, 2, N'1.24.35.41.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (42, 36, N'英德接触网工区', N'', N'', 0, 1, N'1.24.36.42.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (43, 36, N'源潭接触网工区', N'', N'', 0, 2, N'1.24.36.43.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (44, 5, N'车间技术员(副主任)', N'', N'', 1, 3, N'1.24.5.44.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (45, 24, N'分管安全副段长', N'', N'', 1, 3, N'1.24.45.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (46, 24, N'分管生产副段长', N'', N'', 1, 1, N'1.24.46.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (47, 5, N'车间主任', N'', N'', 1, 4, N'1.24.5.47.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (48, 5, N'车间党支部书记', N'', N'', 1, 5, N'1.24.5.48.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (49, 29, N'技术科科长', N'', N'', 1, 1, N'1.24.29.49.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (50, 29, N'技术科主任工程师(副科长)', N'', N'', 1, 2, N'1.24.29.50.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (51, 31, N'安全科科长', N'', N'', 1, 1, N'1.24.31.51.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (52, 31, N'安全科主管安全员(副科长)', N'', N'', 1, 2, N'1.24.31.52.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (53, 24, N'段长', N'', N'', 1, 0, N'1.24.53.')
INSERT [dbo].[OU] ([ID], [PID], [Name], [Address], [Note], [Position], [OUOrder], [Path]) VALUES (54, 24, N'段长', N'', N'', 1, 13, N'1.24.54.')
INSERT [dbo].[OU_Role] ([OU_ID], [Role_ID], [TopOUID]) VALUES (46, 5, -1)
INSERT [dbo].[OU_Role] ([OU_ID], [Role_ID], [TopOUID]) VALUES (53, 5, -1)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 1)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 5)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 6)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 24)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 25)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 26)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 27)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 29)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 31)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 44)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 47)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 50)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (1, 51)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (4, 5)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (4, 47)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (5, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (7, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (9, 6)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (10, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (11, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (12, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (13, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (14, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (15, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (16, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (17, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (18, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (19, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (20, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (21, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (22, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (23, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (23, 24)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (23, 46)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (23, 53)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (24, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (25, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (26, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (27, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (28, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (29, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (30, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (31, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (32, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (33, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (34, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (35, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (36, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (37, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (39, 1)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (40, 1)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (40, 24)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (40, 46)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (40, 53)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (41, 35)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (42, 7)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (43, 24)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (43, 53)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (44, 24)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (44, 45)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (44, 46)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (45, 36)
INSERT [dbo].[OU_User] ([User_ID], [OU_ID]) VALUES (46, 36)
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (1, N'管理员', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (2, N'分公司管理员', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (5, N'施工检查点角色', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (6, N'施工配合流程图发起人', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (7, N'安全科主管安全员(副科长)', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (8, N'安全科科长', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (9, N'技术科主管工程师', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (10, N'技术科科长', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (11, N'车间技术员', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (12, N'车间主任、党支部书记', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (13, N'分管安全副段长', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (14, N'分管生产副段长', N'')
INSERT [dbo].[Role] ([ID], [Name], [Note]) VALUES (15, N'段长', N'')
INSERT [dbo].[Role_Function] ([Role_ID], [Function_ID]) VALUES (1, 1000)
INSERT [dbo].[Role_Function] ([Role_ID], [Function_ID]) VALUES (1, 1001)
INSERT [dbo].[Role_Function] ([Role_ID], [Function_ID]) VALUES (1, 1002)
INSERT [dbo].[Role_Function] ([Role_ID], [Function_ID]) VALUES (1, 1000001)
INSERT [dbo].[Role_Function] ([Role_ID], [Function_ID]) VALUES (1, 1000002)
INSERT [dbo].[Role_Function] ([Role_ID], [Function_ID]) VALUES (1, 1001001)
INSERT [dbo].[Role_Function] ([Role_ID], [Function_ID]) VALUES (1, 1001002)
INSERT [dbo].[Role_Function] ([Role_ID], [Function_ID]) VALUES (1, 1002001)
INSERT [dbo].[SystemAuthorize] ([ID], [SystemType_OID], [Content]) VALUES (1, N'IPSPSD', N'IPSPSD')
INSERT [dbo].[SystemAuthorize] ([ID], [SystemType_OID], [Content]) VALUES (2, N'IPSPSD', N'IPSPBD')
INSERT [dbo].[SystemAuthorize] ([ID], [SystemType_OID], [Content]) VALUES (3, N'IPSPSD', N'ipsp_jcw')
INSERT [dbo].[SystemType] ([OID], [Name], [CustomID], [Authorize], [Note]) VALUES (N'HWSecurity', N'权限管理系统', N'HWDemo', N'', N'')
INSERT [dbo].[SystemType] ([OID], [Name], [CustomID], [Authorize], [Note]) VALUES (N'IPSPSD', N'广铁集团接触网设备巡检系统', N'HWDemo', N'', N'')
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (1, 1, 0)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (2, 3, 0)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (4, 1, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (5, 1, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (6, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (7, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (9, 1, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (10, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (11, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (12, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (13, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (13, 4, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (14, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (15, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (16, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (17, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (18, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (19, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (20, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (21, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (22, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (23, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (24, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (25, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (26, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (27, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (28, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (29, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (30, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (31, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (32, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (33, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (34, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (35, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (36, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (37, 3, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (38, 1, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (39, 1, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (41, 1, -1)
INSERT [dbo].[User_Role] ([User_ID], [Role_ID], [TopOUID]) VALUES (42, 4, -1)
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (1, -1, N'admin', N'MwI2ADUCPAg/Cj4IOQo4AGgCbwBuAmUIZApnCH4KUQBQAlcAVgJdCFwKWwhWClkAWAI=', N'管理员', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (3, -1, N'lizhancong', N'MQA2BDUEPAA1ADoEYQR4EHgQfxR4FHUQdBBzFGQUUQBQAFcEUgRVAF4AWwRWBEkQShA=', N'李展聪', 0, N'', N'', N'', N'', N'', N'', N'', N'', 2, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (4, -1, N'txjxadmin', N'MwIyADECPABnAmIAaQJ4EHgSfxB+EnUQdBJ/EGYSUQBQAlcAUgJdAFwCXwBaAkkQSBI=', N'棠溪检修车间管理员', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (5, -1, N'txadmin', N'MwI2BDUGPAg/CjoMOQ4oAHgCbwRqBmUIZApjDG4OUQBQAlcEUgZdCFwKWwxWDlkAWAI=', N'棠溪网工区管理员', 0, N'', N'', N'', N'', N'', N'', N'', N'', -1, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (7, -1, N'lizc', N'MQAyADEAPAg9CD4IbQhoAGoAawB+AGUIZghnCGoIUQBSAFMAVgBdCF4IXwhSCFkAWgA=', N'李展聪', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (8, -1, N'gzadmin', N'MwI2BDUGPAg/CjoMOQ44AHgCbwRqBmUIZApjDH4OUQBQAlcEUgZdCFwKWwxWDlkAWAI=', N'广州站管理员', 0, N'', N'', N'', N'', N'', N'', N'', N'', 5, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (9, -1, N'gzbadmin', N'MwAyADEANAA1ADoAPwAoEHgQexB+EH0QfhBzEGgQUQBQAFMAVgBVAFYAWwBQAEkQSBA=', N'广州北网工区管理员', 0, N'', N'', N'', N'', N'', N'', N'', N'', -1, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (10, -1, N'liuhao', N'MQA2BDcEPAg9CDoMawxoAHoAbwRoBGUIZghjDGwMUQBSAFcEUARdCF4IWwxUDFkAWgA=', N'刘豪', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (11, -1, N'majun', N'MwIyADECPAg/CjoIbQpoAHgCbwBqAmUIZApzCH4KUQBQAlMAUgJdCFwKXwhSCkkAWAI=', N'马俊', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (12, -1, N'mazy', N'MQAyADEAPAg9CD4IbQh4AGoAawB+AHUIZghnCGoIUQBSAFMAVgBdCF4IXwhSCEkAWgA=', N'马泽宜', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (13, -1, N'卢超杰', N'MwM2BTUHNAFnC2IFYQdoAXgDbxVqB30BbBNrDWYHUQFwA1clUgd1CVQjUwV+D0khWBM=', N'卢超杰', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (14, -1, N'吴惠恒', N'MwI2BDUGNABnAmIEYQZoAHgCfxRqBn0AfBJrBGYGUQBQInckUgZVIHQiUwReJmkgSBI=', N'吴惠恒', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (15, -1, N'刘焕其', N'MwI2BTUHPABnA2IFaQZ4EXgTfxR6F30RdBJ7FWYXcQBwI1cFcgZ1IVQDewR+J0kRaBI=', N'刘焕其', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (16, -1, N'姚仕辉', N'MwM2BDUHPAlnCmoNaQ94AGgDfxV6BmUJdBt7DH4PcQFwAncFcgd1CHwLew1+DnkBaBM=', N'姚仕辉', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (17, -1, N'廖小成', N'MwI2BDUGNAhvCmIEYQ5oEHgCfxRqFn0AfBpjHGYGcQBwAnckcgZ9CHQicwx2DmkgaBI=', N'廖小成', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (18, -1, N'朱雄峰', N'MwM2BDUGNAFnAmIMYQdoEHgSfwVqFn0YfANrFGYeUSFwAlcEUid1AFQKUyV+BkkQSCM=', N'朱雄峰', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (19, -1, N'梁诚荣', N'MwI2BTUHNAhvC2IFYQ54AWgDbwR6B20BbApzDXYHcSBwA1cFciZ9CVQDcyx2D1kBeCI=', N'梁诚荣', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (20, -1, N'林勇', N'MwM2BDUHNABnA2IEYQdoEHgDbxR6B20QfANrFGYHcQBwI3cEcid1AHQjcwR+J3kQaCM=', N'林勇', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (21, -1, N'黄维权', N'MwI2BDUHNAhnCmIFYQ54EGgDbxR6Fm0BbBp7HHYHcQBQInclcgZVKHQjcwxeLnkheBI=', N'黄维权', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (22, -1, N'罗玉金', N'MwM2BTUHNAlvA2IFYQ9oEXgTfxVqF30RfBtjFWYXcSFwI3cFcid9IXQDcy12J2kRaDM=', N'罗玉金', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (23, -1, N'潘锡全', N'MwM2BTUHPAlnA2oFaQ9oEWgTfwVqF2URdAtrFX4XcSFQA1cFcidVAVwDey1eB1kRaCM=', N'潘锡全', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (24, -1, N'肖江平', N'MwI2BDUGNABvCmIMYQZ4AHgSfwR6Bn0YfAJzDGYecQBwIlcEcgZ9KFQKcwR2LkkQaAI=', N'肖江平', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (25, -1, N'姚柏林', N'MwM2BTUHPAlvA2IFaQ9oAXgDfxVqB30BdBtjBWYHcQFwI3clcgd9IXQjew12J2khaBM=', N'姚柏林', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (26, -1, N'尚丰杰', N'MwI2BDUHPAhnCmIFaQ54AHgDfxR6Bn0BdBp7DGYHcQBQAlclcgZVCFQjewxeDkkhaBI=', N'尚丰杰', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (27, -1, N'黄清华', N'MwI2BDUHNAhnCmoFYQ5oAGgTbxRqBmURbBprDH4XcQBwIncFcgZ1KHwDcwx+LnkReBI=', N'黄清华', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (28, -1, N'陆智财', N'MwI2BDUHNABvAmINYQZ4AGgDbxR6Bm0JbBJzBHYPcQBQIlcFcgZdIFQLcwRWJlkBeBI=', N'陆智财', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (29, -1, N'侯达昌', N'MwM2BTUGPAlvC2oEaQ94AWgCbwV6B2UAZAtzDX4GUQFQA3ckUgddCXwiWw1WD3kgWAM=', N'侯达昌', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (30, -1, N'涂桂清', N'MwM2BDUGNAlnCmIMYQ9oAGgCbwVqBm0IbAtrDHYOcSFwInckcid1KHQqcy1+LnkgeCM=', N'涂桂清', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (31, -1, N'陈东', N'MQI2BDcGPAhtAmoMawZ4AGoSfwRoFnUIZhJzDHwWcQByAncEcAZ9CH4Cewx0BmkAehI=', N'陈东', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (32, -1, N'谢锡荣', N'MwI2BTUHNAhnA2IFYQ5oEWgDbwRqF20BbAprFXYHUQBQA1cFUgZVAVQDUwxeB1kBWAI=', N'谢锡荣', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (33, -1, N'黄荣聪', N'MwI2BTUGNAhnA2oEYQ5oAWgCbxRqB2UAbBprBX4GcQBQA1cEcgZVAVwCcwxeB1kAeBI=', N'黄荣聪', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (34, -1, N'凌辉煌', N'MwM2BTUHPAFvC2oFaQdoAWgTbxVqB2URZBNjDX4XcQFwA3clcgd9CXwjewV2D3kxeBM=', N'凌辉煌', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (35, -1, N'余强', N'MQM2BTcHPAltC2oNaw94EXoDfxV4B3UZdgtzHWwPUQFyA1cFcAddCX4LWw10D0kRagM=', N'余强', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (36, -1, N'曾柏康', N'MwI2BTUGPABvA2IMaQZoAXgSfwRqB30YdAJjBWYeUSBwI1cEUiZ9IVQKWyR2J0kQSCI=', N'曾柏康', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (37, -1, N'王鑫', N'MwM2BDUHPABvA2oEaQdoEGgTbxRqF2UQZBNjFH4XUQBwI1cEciddAHwjWwR2J1kQeDM=', N'王鑫', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (39, -1, N'供电处', N'OgM8BT4HMAk6AzwNaQ94EWgTfwV6F20ZdAt7FXYfcQFQI3cFcgdVIXQLew1eJ3kRaAM=', N'供电处', 0, N'', N'', N'', N'', N'', N'', N'', N'', -1, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (40, -1, N'潘光辉', N'MgM0BTYHOAk6AzwNaQ9oEWgDfwVqF2UJdAtjFX4PcSFwA3cFcid9AXwLey12B3kBaCM=', N'潘光辉', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (41, -1, N'wangdh', N'MwA2BDUEPAg9CDoMOQw4AGgAbwRoBGUIdAhjDH4MUQBSAFcEUgRdCFwIWwxUDFkASAA=', N'王东鸿', 0, N'', N'', N'', N'', N'', N'', N'', N'', -1, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (42, -1, N'巡视路径管理员', N'MwM2BTUHPAlvC2oMaQ5oEWgDbwVqF2UZZBpzHH4fcQFQA3cFUid9KHwKWw12D1kBeBM=', N'巡视路径管理员', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (43, -1, N'段长', N'MQM2BTcHPAFlC2oFYw94EXoDfxV4B3URfgtzFWQPUQFSI1cFUCddAVYrWwVcL0kRSiM=', N'段长', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (44, -1, N'分管生产副段长', N'MwI2BTUHPAhvCmoNaQ9oEGgTfxVqBmUYdAtzHX4eUSFwI1cEUgZdKVwLewxWL2kxWAI=', N'分管生产副段长', 0, N'', N'', N'', N'', N'', N'', N'', N'', 24, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (45, -1, N'英德车间技术员', N'MwJmBWUHbAlvCmoNaQ54AHgTbwV6F2UIZAtzHG4OUQFQA1cFciZdKXwKWwxWD1kBSBM=', N'英德车间技术员', 0, N'', N'', N'', N'', N'', N'', N'', N'', -1, N'')
INSERT [dbo].[Users] ([ID], [PID], [Name], [Password], [FullName], [IsExpire], [Title], [IdentityCard], [MobilePhone], [OfficePhone], [HomePhone], [Email], [Address], [CustomField], [TopOUID], [Remark]) VALUES (46, -1, N'英德车间主任', N'MQJmBWUHbAlvCmoMaw54EWgDfxV6BnUIdgpzHX4PUQFQAlcEUAZdCVwLWw1WDkkASgI=', N'英德车间主任', 0, N'', N'', N'', N'', N'', N'', N'', N'', -1, N'')
ALTER TABLE [dbo].[Functions] ADD  CONSTRAINT [DF_Function_PID]  DEFAULT ((-1)) FOR [PID]
GO
ALTER TABLE [dbo].[Functions] ADD  CONSTRAINT [DF_Function_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[Functions] ADD  CONSTRAINT [DF_Function_URL]  DEFAULT ('') FOR [ControlID]
GO
ALTER TABLE [dbo].[Functions] ADD  CONSTRAINT [DF_Function_SystemType]  DEFAULT ('') FOR [SystemType_ID]
GO
ALTER TABLE [dbo].[Functions] ADD  CONSTRAINT [DF_Functions_Path]  DEFAULT ('') FOR [Path]
GO
ALTER TABLE [dbo].[Functions] ADD  CONSTRAINT [DF_Functions_FunctionType]  DEFAULT ((2)) FOR [FunctionType]
GO
ALTER TABLE [dbo].[Functions] ADD  CONSTRAINT [DF_Functions_Sort]  DEFAULT ((1)) FOR [Sort]
GO
ALTER TABLE [dbo].[OU] ADD  CONSTRAINT [DF_Group_PID]  DEFAULT ((-1)) FOR [PID]
GO
ALTER TABLE [dbo].[OU] ADD  CONSTRAINT [DF_OU_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[OU] ADD  CONSTRAINT [DF_OU_Address]  DEFAULT ('') FOR [Address]
GO
ALTER TABLE [dbo].[OU] ADD  CONSTRAINT [DF_OU_Note]  DEFAULT ('') FOR [Note]
GO
ALTER TABLE [dbo].[OU] ADD  CONSTRAINT [DF_OU_Position_1]  DEFAULT ((0)) FOR [Position]
GO
ALTER TABLE [dbo].[OU] ADD  CONSTRAINT [DF_OU_OUOrder]  DEFAULT ((-1)) FOR [OUOrder]
GO
ALTER TABLE [dbo].[OU] ADD  CONSTRAINT [DF_OU_Path]  DEFAULT ('') FOR [Path]
GO
ALTER TABLE [dbo].[OU_Role] ADD  CONSTRAINT [DF_OU_Role_OU_ID]  DEFAULT ((-1)) FOR [OU_ID]
GO
ALTER TABLE [dbo].[OU_Role] ADD  CONSTRAINT [DF_OU_Role_TopOUID]  DEFAULT ((-1)) FOR [TopOUID]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_Note]  DEFAULT ('') FOR [Note]
GO
ALTER TABLE [dbo].[SystemAuthorize] ADD  CONSTRAINT [DF_SystemAuthorize_SystemType_ID]  DEFAULT ('') FOR [SystemType_OID]
GO
ALTER TABLE [dbo].[SystemAuthorize] ADD  CONSTRAINT [DF_SystemAuthorize_Content]  DEFAULT ('') FOR [Content]
GO
ALTER TABLE [dbo].[SystemType] ADD  CONSTRAINT [DF_SystemType_OID]  DEFAULT ('') FOR [OID]
GO
ALTER TABLE [dbo].[SystemType] ADD  CONSTRAINT [DF_SystemType_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[SystemType] ADD  CONSTRAINT [DF_SystemType_CustomID]  DEFAULT ('HWDemo') FOR [CustomID]
GO
ALTER TABLE [dbo].[SystemType] ADD  CONSTRAINT [DF_SystemType_Authorize]  DEFAULT ('') FOR [Authorize]
GO
ALTER TABLE [dbo].[SystemType] ADD  CONSTRAINT [DF_SystemType_Note]  DEFAULT ('') FOR [Note]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_PID]  DEFAULT ((-1)) FOR [PID]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_Password]  DEFAULT ('') FOR [Password]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_FullName]  DEFAULT ('') FOR [FullName]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_IsExpire]  DEFAULT ((0)) FOR [IsExpire]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_Title]  DEFAULT ('') FOR [Title]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_IdentityCard]  DEFAULT ('') FOR [IdentityCard]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_MobilePhone]  DEFAULT ('') FOR [MobilePhone]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_OfficePhone]  DEFAULT ('') FOR [OfficePhone]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_HomePhone]  DEFAULT ('') FOR [HomePhone]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_Email]  DEFAULT ('') FOR [Email]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_Address]  DEFAULT ('') FOR [Address]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_CustomField]  DEFAULT ('') FOR [CustomField]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_TopOUID]  DEFAULT ((-1)) FOR [TopOUID]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Remark]  DEFAULT ('') FOR [Remark]
GO
ALTER TABLE [dbo].[Functions]  WITH NOCHECK ADD  CONSTRAINT [FK_Function_Function] FOREIGN KEY([PID])
REFERENCES [dbo].[Functions] ([ID])
GO
ALTER TABLE [dbo].[Functions] NOCHECK CONSTRAINT [FK_Function_Function]
GO
ALTER TABLE [dbo].[Functions]  WITH CHECK ADD  CONSTRAINT [FK_Function_SystemType] FOREIGN KEY([SystemType_ID])
REFERENCES [dbo].[SystemType] ([OID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Functions] CHECK CONSTRAINT [FK_Function_SystemType]
GO
ALTER TABLE [dbo].[OU]  WITH NOCHECK ADD  CONSTRAINT [FK_OU_OU] FOREIGN KEY([PID])
REFERENCES [dbo].[OU] ([ID])
GO
ALTER TABLE [dbo].[OU] NOCHECK CONSTRAINT [FK_OU_OU]
GO
ALTER TABLE [dbo].[OU_Role]  WITH CHECK ADD  CONSTRAINT [FK_OU_Role_OU] FOREIGN KEY([OU_ID])
REFERENCES [dbo].[OU] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OU_Role] CHECK CONSTRAINT [FK_OU_Role_OU]
GO
ALTER TABLE [dbo].[OU_Role]  WITH CHECK ADD  CONSTRAINT [FK_OU_Role_Role] FOREIGN KEY([Role_ID])
REFERENCES [dbo].[Role] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OU_Role] CHECK CONSTRAINT [FK_OU_Role_Role]
GO
ALTER TABLE [dbo].[OU_User]  WITH CHECK ADD  CONSTRAINT [FK_OU_User_OU] FOREIGN KEY([OU_ID])
REFERENCES [dbo].[OU] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OU_User] CHECK CONSTRAINT [FK_OU_User_OU]
GO
ALTER TABLE [dbo].[OU_User]  WITH CHECK ADD  CONSTRAINT [FK_User_OU_User] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OU_User] CHECK CONSTRAINT [FK_User_OU_User]
GO
ALTER TABLE [dbo].[Role_Function]  WITH CHECK ADD  CONSTRAINT [FK_Role_Function_Function] FOREIGN KEY([Function_ID])
REFERENCES [dbo].[Functions] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Role_Function] CHECK CONSTRAINT [FK_Role_Function_Function]
GO
ALTER TABLE [dbo].[Role_Function]  WITH CHECK ADD  CONSTRAINT [FK_Role_Function_Role] FOREIGN KEY([Role_ID])
REFERENCES [dbo].[Role] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Role_Function] CHECK CONSTRAINT [FK_Role_Function_Role]
GO
ALTER TABLE [dbo].[SystemAuthorize]  WITH CHECK ADD  CONSTRAINT [FK_SystemAuthorize_SystemType] FOREIGN KEY([SystemType_OID])
REFERENCES [dbo].[SystemType] ([OID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemAuthorize] CHECK CONSTRAINT [FK_SystemAuthorize_SystemType]
GO
ALTER TABLE [dbo].[Users]  WITH NOCHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([PID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Users] NOCHECK CONSTRAINT [FK_User_User]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Functions', @level2type=N'COLUMN',@level2name=N'Path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否岗位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OU', @level2type=N'COLUMN',@level2name=N'Position'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OU', @level2type=N'COLUMN',@level2name=N'OUOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OU', @level2type=N'COLUMN',@level2name=N'Path'
GO
USE [master]
GO
ALTER DATABASE [BusinessRights] SET  READ_WRITE 
GO
