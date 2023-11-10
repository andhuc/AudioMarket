USE [master]
GO
/****** Object:  Database [AudioMarket]    Script Date: 10/27/2023 1:22:58 PM ******/
CREATE DATABASE [AudioMarket]
GO
ALTER DATABASE [AudioMarket] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AudioMarket].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AudioMarket] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AudioMarket] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AudioMarket] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AudioMarket] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AudioMarket] SET ARITHABORT OFF 
GO
ALTER DATABASE [AudioMarket] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AudioMarket] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AudioMarket] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AudioMarket] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AudioMarket] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AudioMarket] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AudioMarket] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AudioMarket] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AudioMarket] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AudioMarket] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AudioMarket] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AudioMarket] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AudioMarket] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AudioMarket] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AudioMarket] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AudioMarket] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AudioMarket] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AudioMarket] SET RECOVERY FULL 
GO
ALTER DATABASE [AudioMarket] SET  MULTI_USER 
GO
ALTER DATABASE [AudioMarket] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AudioMarket] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AudioMarket] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AudioMarket] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AudioMarket] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AudioMarket] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'AudioMarket', N'ON'
GO
ALTER DATABASE [AudioMarket] SET QUERY_STORE = ON
GO
ALTER DATABASE [AudioMarket] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [AudioMarket]
GO
/****** Object:  Table [dbo].[Audio]    Script Date: 10/27/2023 1:22:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audio](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[artistId] [int] NULL,
	[genreId] [int] NULL,
	[moodId] [int] NULL,
	[status] [bit] DEFAULT 1,
	[filename] [nvarchar](255) NULL,
	[image] [nvarchar](255) NULL,
	[title] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 10/27/2023 1:22:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[discountAmount] [decimal](18, 2) NULL,
	[quantity] [int] NULL,
	[status] [bit] DEFAULT 1,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorite]    Script Date: 10/27/2023 1:22:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorite](
	[userId] [int] NOT NULL,
	[audioId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC,
	[audioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 10/27/2023 1:22:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[genre] [nvarchar](255) NULL,
	[status] [bit] DEFAULT 1,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mood]    Script Date: 10/27/2023 1:22:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mood](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[moodName] [nvarchar](255) NULL,
	[status] [bit] DEFAULT 1,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 10/27/2023 1:22:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[buyerId] [int] NULL,
	[audioId] [int] NULL,
	[status] [int] DEFAULT 0,
	[purchaseDate] [datetime] DEFAULT GETDATE(),
	[discountId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 10/27/2023 1:22:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[audioId] [int] NULL,
	[rating] [int] NULL,
	[comment] [nvarchar](max) NULL,
	[status] [int] DEFAULT 0,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/27/2023 1:22:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NULL,
	[password] [nvarchar](255) NULL,
	[role] [int] DEFAULT 0,
	[name] [nvarchar](255) NULL,
	[status] [bit] DEFAULT 1,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Audio]  WITH CHECK ADD FOREIGN KEY([artistId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Audio]  WITH CHECK ADD FOREIGN KEY([genreId])
REFERENCES [dbo].[Genre] ([id])
GO
ALTER TABLE [dbo].[Audio]  WITH CHECK ADD FOREIGN KEY([moodId])
REFERENCES [dbo].[Mood] ([id])
GO
ALTER TABLE [dbo].[Favorite]  WITH CHECK ADD FOREIGN KEY([audioId])
REFERENCES [dbo].[Audio] ([id])
GO
ALTER TABLE [dbo].[Favorite]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([audioId])
REFERENCES [dbo].[Audio] ([id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([buyerId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([discountId])
REFERENCES [dbo].[Discount] ([id])
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([audioId])
REFERENCES [dbo].[Audio] ([id])
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
USE [master]
GO
ALTER DATABASE [AudioMarket] SET  READ_WRITE 
GO

USE [AudioMarket]
GO

-- Users
INSERT INTO [User] (username, password, name, role)
VALUES
  ('john123', 'password123', 'John Doe', 0),
  ('jane456', 'password456', 'Jane Smith', 0),
  ('mike789', 'password789', 'Mike Jones', 0),
  ('user', '1', 'Chris Lee', 0),
  ('admin', 'admin', 'Admin', 1);

-- Genres 
INSERT INTO Genre (genre)
VALUES
  ('Pop'),
  ('Rock'),
  ('Jazz'),
  ('Classical'),
  ('Hip Hop');
  
-- Moods
INSERT INTO Mood (moodName) 
VALUES
  ('Happy'),
  ('Sad'), 
  ('Energetic'),
  ('Calm'),
  ('Angry');
  
-- Audio
INSERT INTO Audio (artistId, genreId, moodId, filename, title, [image])
VALUES
  (4, 2, 1, '/audio/CANDY.mp3', 'Candy', '/audio/img/1.gif'),
  (4, 5, 3, '/audio/MARVEL.mp3', 'Marvel', '/audio/img/2.png'),
  (4, 1, 2, '/audio/LILY.mp3', 'Sakura', '/audio/img/3.png'),
  (3, 4, 4, '/audio/LUCKY.mp3', 'Lucky', '/audio/img/4.png'),
  (2, 3, 5, '/audio/PEACH.mp3', 'lau dai tinh ai', '/audio/img/5.png');

INSERT INTO Audio (artistId, genreId, moodId, filename, title, [image])
VALUES
  (3, 3, 1, '/audio/ADORE.mp3', 'Adore', '/audio/img/6.gif'),
  (2, 5, 3, '/audio/BLAME.mp3', 'Blame', '/audio/img/7.png'),
  (1, 2, 2, '/audio/D.A.D.mp3', 'D.A.D', '/audio/img/8.png'),
  (3, 4, 1, '/audio/FRIEND.mp3', 'Friend', '/audio/img/9.png'),
  (2, 3, 5, '/audio/HOONEY.mp3', 'Hooney', '/audio/img/10.png');
  
-- Reviews
INSERT INTO Review (userId, audioId, rating, comment)  
VALUES
  (1, 1, 5, 'Awesome song!'),
  (2, 1, 4, 'It was ok'),
  (3, 2, 3, 'I did not like it'),
  (1, 3, 5, 'Her best song!'),
  (2, 5, 5, 'Great jazz tune');

-- Discounts
INSERT INTO Discount (discountAmount, quantity)
VALUES
  (0.1, 2),
  (0.2, 3);
  
-- Orders  
INSERT INTO [Order] (buyerId, audioId, status, discountId)
VALUES
  (1, 1, 0, null),
  (2, 2, 0, null),
  (3, 3, 1, null),
  (1, 4, 2, 1),
  (2, 5, 0, 2);
  
-- Favorites
INSERT INTO Favorite (userId, audioId)
VALUES 
  (1, 1),
  (1, 3),
  (2, 2),
  (2, 5),
  (3, 4),
  (1, 5);

INSERT INTO Favorite (userId, audioId)
VALUES 
  (4, 1),
  (4, 6),
  (2, 4),
  (4, 5),
  (3, 8),
  (2, 9);

INSERT INTO Favorite (userId, audioId)
VALUES 
  (4, 1),
  (3, 7),
  (2, 1),
  (2, 7),
  (4, 4),
  (1, 10);