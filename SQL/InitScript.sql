CREATE DATABASE ORM
GO

USE ORM
GO

CREATE TABLE [dbo].[Models](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Int] [int] NULL,
	[Nvarchar255] [nvarchar](255) NULL,
	[NvarcharMax] [nvarchar](max) NULL,
	[Ntext] [ntext] NULL,
	[DateTime] [datetime] NULL,
	[Money] [money] NULL,
	[Bit] [bit] NULL,
 CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED 
([Id] ASC)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[DeleteItem]

	@Id INT	
AS
BEGIN
	SET NOCOUNT ON;

	DELETE Models WHERE Id = @Id;
END
GO

CREATE PROCEDURE [dbo].[GetModels]

AS
BEGIN
	SELECT * FROM Models
END
GO

CREATE PROCEDURE [dbo].[InsertModel]

	@Id INT OUTPUT,
	@Int INT = NULL,
	@Nvarchar255 NVARCHAR(255) = NULL,
	@NvarcharMax NVARCHAR(MAX) = NULL,
	@Ntext NTEXT = NULL,
	@DateTime DATETIME = NULL,
	@Money MONEY = NULL,
	@Bit BIT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Models ([Int],[Nvarchar255],[NvarcharMax],[Ntext],[DateTime],[Money],[Bit])
	VALUES (@Int, @Nvarchar255, @NvarcharMax, @Ntext, @DateTime, @Money, @Bit)
	
	SET @Id = @@IDENTITY;
END
GO

CREATE PROCEDURE [dbo].[Truncate]
	
AS
BEGIN
	SET NOCOUNT ON;

	TRUNCATE TABLE Models
END
GO

--INSERT [Models] ([Int], [Nvarchar255], [NvarcharMax], [Ntext], [DateTime], [Money], [Bit]) VALUES (1, N'nvarchar 255', N'nvarchar max', N'ntext', CAST(0x0000A13900000000 AS DateTime), 123.4500, 1)
--INSERT [Models] ([Int], [Nvarchar255], [NvarcharMax], [Ntext], [DateTime], [Money], [Bit]) VALUES (NULL, NULL, NULL, NULL, NULL, NULL, NULL)

