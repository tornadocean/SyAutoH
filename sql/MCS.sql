USE [master]
GO
/****** Object:  Database [MCS]    Script Date: 2013/1/5 10:15:13 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'MCS')
DROP DATABASE [MCS]
GO
/****** Object:  Database [MCS]    Script Date: 2012/12/18 17:38:14 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'MCS')
BEGIN
CREATE DATABASE [MCS]
END
GO
ALTER DATABASE [MCS] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MCS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MCS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MCS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MCS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MCS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MCS] SET ARITHABORT OFF 
GO
ALTER DATABASE [MCS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MCS] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MCS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MCS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MCS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MCS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MCS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MCS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MCS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MCS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MCS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MCS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MCS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MCS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MCS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MCS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MCS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MCS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MCS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MCS] SET  MULTI_USER 
GO
ALTER DATABASE [MCS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MCS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MCS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MCS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [MCS]
GO
/****** Object:  Table [dbo].[Config]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Config](
	[Name] [varchar](50) NOT NULL,
	[IntVal] [int] NULL,
	[StrVal] [varchar](50) NULL,
 CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConfigTypeLane]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConfigTypeLane](
	[TypeID] [tinyint] NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
	[TypeDesc] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DevStatus]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DevStatus](
	[ID] [int] NOT NULL,
	[Type] [nchar](10) NULL,
	[Status] [nchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Foup]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Foup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Location] [int] NOT NULL,
	[LocationType] [tinyint] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[Lot] [int] NOT NULL,
	[BarCode] [int] NOT NULL,
	[Carrier] [int] NOT NULL,
	[Port] [int] NOT NULL,
 CONSTRAINT [PK_Foup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KeyPoints]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KeyPoints](
	[Position] [int] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[SpeedRate] [tinyint] NOT NULL,
	[TeachMode] [tinyint] NOT NULL,
	[OHT_ID] [tinyint] NOT NULL,
	[Lane_ID] [int] NOT NULL,
	[Prev] [int] NOT NULL,
	[Next] [int] NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_KeyPoints] PRIMARY KEY CLUSTERED 
(
	[Position] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Lane]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lane](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Start] [int] NOT NULL,
	[Finish] [int] NOT NULL,
	[Prev] [int] NOT NULL,
	[Next] [int] NOT NULL,
	[Next_Frok] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[MapID] [int] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_PathInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LinkSession]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LinkSession](
	[SessionID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RealRight] [int] NOT NULL,
	[OrginRight] [int] NOT NULL,
	[AccessTime] [datetime] NOT NULL,
	[ConnectInfo] [varchar](200) NOT NULL,
	[UserStatus] [int] NOT NULL,
 CONSTRAINT [PK_LinkSession] PRIMARY KEY CLUSTERED 
(
	[SessionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MapInfo]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MapInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Descript] [ntext] NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_MapInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[McsUser]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[McsUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[UserRight] [int] NOT NULL,
 CONSTRAINT [PK_McsUser] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OHVLocal]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OHVLocal](
	[ID] [int] NULL,
	[PosFrom] [int] NULL,
	[PosTo] [int] NULL,
	[FoupID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OptsRights]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OptsRights](
	[OPT] [varchar](50) NOT NULL,
	[MODE] [int] NOT NULL,
	[RoleRight] [int] NOT NULL,
 CONSTRAINT [PK_OptsRights] PRIMARY KEY CLUSTERED 
(
	[OPT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RightInfo]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RightInfo](
	[ID] [int] NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_RightInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stocker]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stocker](
	[ID] [int] NULL,
	[Max] [int] NULL,
	[Free] [int] NULL,
	[Status] [nchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TransCommand]    Script Date: 2013/1/5 10:15:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransCommand](
	[Command] [nchar](10) NULL,
	[PosFrom] [int] NULL,
	[PosTo] [int] NULL,
	[FoupID] [int] NULL,
	[Status] [int] NULL
) ON [PRIMARY]

GO
INSERT [dbo].[Config] ([Name], [IntVal], [StrVal]) VALUES (N'MapInUse', 1, NULL)
INSERT [dbo].[Config] ([Name], [IntVal], [StrVal]) VALUES (N'MapNameInUse', NULL, N'LoopTest')
INSERT [dbo].[ConfigTypeLane] ([TypeID], [TypeName], [TypeDesc]) VALUES (1, N'line', NULL)
INSERT [dbo].[ConfigTypeLane] ([TypeID], [TypeName], [TypeDesc]) VALUES (2, N'curve', NULL)
INSERT [dbo].[ConfigTypeLane] ([TypeID], [TypeName], [TypeDesc]) VALUES (3, N'fork', NULL)
SET IDENTITY_INSERT [dbo].[Foup] ON 

INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (1, -1, 1, 0, 0, 3100, 1, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (2, -1, 1, 0, 0, 3003, 1, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (3, -1, 1, 0, 0, 3002, 1, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (4, -1, 1, 0, 0, 2001, 2, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (5, -1, 1, 0, 0, 1120, 3, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (6, -1, 1, 0, 0, 2000, 2, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (7, -1, 1, 0, 0, 1110, 3, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (8, -1, 1, 0, 0, 1100, 3, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (9, -1, 1, 0, 0, 1003, 3, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (10, -1, 1, 0, 0, 1002, 3, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (11, -1, 1, 0, 0, 1001, 3, 5)
INSERT [dbo].[Foup] ([ID], [Location], [LocationType], [Status], [Lot], [BarCode], [Carrier], [Port]) VALUES (12, -1, 1, 0, 0, 1000, 3, 5)
SET IDENTITY_INSERT [dbo].[Foup] OFF
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (1, 1, 10, 1, 1, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (30, 2, 30, 1, 1, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (50, 4, 50, 1, 1, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (70, 8, 70, 1, 1, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (80, 1, 50, 1, 100, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (100, 1, 50, 1, 128, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (120, 32, 10, 1, 3, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (150, 1, 40, 1, 3, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (200, 1, 60, 1, 1, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (400, 4, 60, 1, 1, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (410, 64, 10, 1, 3, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (1000, 1, 50, 1, 200, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (1010, 128, 10, 1, 3, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (1050, 4, 50, 1, 253, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (1250, 8, 50, 1, 253, 0, 0, 0, NULL)
INSERT [dbo].[KeyPoints] ([Position], [Type], [SpeedRate], [TeachMode], [OHT_ID], [Lane_ID], [Prev], [Next], [Name]) VALUES (2000, 1, 50, 1, 14, 0, 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[Lane] ON 

INSERT [dbo].[Lane] ([id], [Start], [Finish], [Prev], [Next], [Next_Frok], [Length], [MapID], [Type], [Enable]) VALUES (1, 0, 50, -1, 3, -1, 50, 1, 2, 1)
INSERT [dbo].[Lane] ([id], [Start], [Finish], [Prev], [Next], [Next_Frok], [Length], [MapID], [Type], [Enable]) VALUES (3, 50, 450, 1, 5, -1, 400, 1, 1, 1)
INSERT [dbo].[Lane] ([id], [Start], [Finish], [Prev], [Next], [Next_Frok], [Length], [MapID], [Type], [Enable]) VALUES (5, 450, 500, 3, 6, -1, 50, 1, 2, 1)
INSERT [dbo].[Lane] ([id], [Start], [Finish], [Prev], [Next], [Next_Frok], [Length], [MapID], [Type], [Enable]) VALUES (6, 500, 600, 5, 7, -1, 100, 1, 1, 1)
INSERT [dbo].[Lane] ([id], [Start], [Finish], [Prev], [Next], [Next_Frok], [Length], [MapID], [Type], [Enable]) VALUES (7, 600, 650, 6, 8, -1, 50, 1, 2, 1)
INSERT [dbo].[Lane] ([id], [Start], [Finish], [Prev], [Next], [Next_Frok], [Length], [MapID], [Type], [Enable]) VALUES (8, 650, 1050, 7, 12, -1, 400, 1, 1, 1)
INSERT [dbo].[Lane] ([id], [Start], [Finish], [Prev], [Next], [Next_Frok], [Length], [MapID], [Type], [Enable]) VALUES (12, 1050, 1100, 8, 13, -1, 50, 1, 2, 1)
INSERT [dbo].[Lane] ([id], [Start], [Finish], [Prev], [Next], [Next_Frok], [Length], [MapID], [Type], [Enable]) VALUES (13, 1100, 1200, 12, 1, -1, 100, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Lane] OFF
SET IDENTITY_INSERT [dbo].[LinkSession] ON 

INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1, 1, 4, 4, CAST(0x0000A12D00E21238 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:43284', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (2, 1, 4, 4, CAST(0x0000A12D00E50BF0 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:44348', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3, 1, 4, 4, CAST(0x0000A12D00E5CE78 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:44781', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (4, 1, 4, 4, CAST(0x0000A12D00FABC48 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:51854', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1003, 1, 4, 4, CAST(0x0000A131012B46D8 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:11674', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1004, 1, 4, 4, CAST(0x0000A131012DFDC4 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:11874', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1005, 1, 4, 4, CAST(0x0000A131013573EC AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:16313', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1006, 1, 4, 4, CAST(0x0000A13101394FD0 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:18307', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1007, 1, 4, 4, CAST(0x0000A1310139767C AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:18327', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1008, 1, 4, 4, CAST(0x0000A13300E0CA18 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:10746', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1009, 1, 4, 4, CAST(0x0000A13600D7D264 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:65282', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1010, 1, 4, 4, CAST(0x0000A13600E7C78C AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:1774', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1011, 1, 4, 4, CAST(0x0000A13600EC5A40 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:2052', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1013, 1, 4, 4, CAST(0x0000A13600EDEB08 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:2210', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (1014, 1, 4, 4, CAST(0x0000A13600F196E0 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:2516', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (2008, 1, 4, 4, CAST(0x0000A13C00A5F1A4 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:8373', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (2011, 1, 4, 4, CAST(0x0000A13C00AB3EAC AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:9715', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (2012, 1, 4, 4, CAST(0x0000A13C00ADC6B8 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:9998', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (2015, 1, 4, 4, CAST(0x0000A13C00BEE0D8 AS DateTime), N'local address = 127.0.0.1:21210

remote address = 127.0.0.1:11806', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (2016, 1, 4, 4, CAST(0x0000A13C00C26D0C AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:12200', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3016, 1, 4, 4, CAST(0x0000A13C00E37C54 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:1542', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3021, 1, 4, 4, CAST(0x0000A13C00FE568C AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:7062', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3022, 1, 4, 4, CAST(0x0000A13C00FEDD50 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:7111', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3023, 1, 4, 4, CAST(0x0000A13C010019B8 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:7250', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3024, 1, 4, 4, CAST(0x0000A13C010315C8 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:7571', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3036, 1, 4, 4, CAST(0x0000A13C0112509C AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:9140', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3039, 1, 4, 4, CAST(0x0000A13D008EE2E8 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:1718', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3040, 1, 4, 4, CAST(0x0000A13D00929F28 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:2125', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3041, 1, 4, 4, CAST(0x0000A13D00944760 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:2294', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3042, 1, 4, 4, CAST(0x0000A13D00971364 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:2604', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3043, 1, 4, 4, CAST(0x0000A13D0097ACE8 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:2697', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3044, 1, 4, 4, CAST(0x0000A13D009CE604 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:3255', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3045, 1, 4, 4, CAST(0x0000A13D009E53A4 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:3415', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3046, 1, 4, 4, CAST(0x0000A13D009F0948 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:3498', 1)
INSERT [dbo].[LinkSession] ([SessionID], [UserID], [RealRight], [OrginRight], [AccessTime], [ConnectInfo], [UserStatus]) VALUES (3048, 1, 4, 4, CAST(0x0000A13D00A70760 AS DateTime), N'local address = 127.0.0.1:21210
remote address = 127.0.0.1:5031', 1)
SET IDENTITY_INSERT [dbo].[LinkSession] OFF
SET IDENTITY_INSERT [dbo].[MapInfo] ON 

INSERT [dbo].[MapInfo] ([ID], [Name], [Descript], [CreateTime]) VALUES (1, N'Single Loop', N'test rail for local', CAST(0x0000A12B00DF9260 AS DateTime))
SET IDENTITY_INSERT [dbo].[MapInfo] OFF
SET IDENTITY_INSERT [dbo].[McsUser] ON 

INSERT [dbo].[McsUser] ([id], [Name], [Password], [UserRight]) VALUES (1, N'admin', N'ad53c70e673460a260d12cdfb59e43eb9a5b7f9b', 4)
SET IDENTITY_INSERT [dbo].[McsUser] OFF
INSERT [dbo].[OptsRights] ([OPT], [MODE], [RoleRight]) VALUES (N'OHT.POS', 0, 3)
INSERT [dbo].[OptsRights] ([OPT], [MODE], [RoleRight]) VALUES (N'OHT.STS', 0, 3)
INSERT [dbo].[OptsRights] ([OPT], [MODE], [RoleRight]) VALUES (N'STK.FOUP', 0, 3)
INSERT [dbo].[RightInfo] ([ID], [RoleName]) VALUES (1, N'NoRight')
INSERT [dbo].[RightInfo] ([ID], [RoleName]) VALUES (2, N'Viewer')
INSERT [dbo].[RightInfo] ([ID], [RoleName]) VALUES (3, N'Guest')
INSERT [dbo].[RightInfo] ([ID], [RoleName]) VALUES (4, N'Operator')
INSERT [dbo].[RightInfo] ([ID], [RoleName]) VALUES (5, N'Builder')
INSERT [dbo].[RightInfo] ([ID], [RoleName]) VALUES (6, N'Admin')
INSERT [dbo].[RightInfo] ([ID], [RoleName]) VALUES (7, N'SuperAdmin')
/****** Object:  Index [IX_BarCode]    Script Date: 2013/1/5 10:15:14 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_BarCode] ON [dbo].[Foup]
(
	[BarCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Name]    Script Date: 2013/1/5 10:15:14 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Name] ON [dbo].[McsUser]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [MCS] SET  READ_WRITE 
GO
