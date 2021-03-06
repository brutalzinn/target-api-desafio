USE [master]
GO
/****** Object:  Database [targetapidb]    Script Date: 25/10/2021 17:04:55 ******/
CREATE DATABASE [targetapidb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'targetapidb', FILENAME = N'/var/opt/mssql/data/targetapidb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'targetapidb_log', FILENAME = N'/var/opt/mssql/data/targetapidb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [targetapidb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [targetapidb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [targetapidb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [targetapidb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [targetapidb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [targetapidb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [targetapidb] SET ARITHABORT OFF 
GO
ALTER DATABASE [targetapidb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [targetapidb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [targetapidb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [targetapidb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [targetapidb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [targetapidb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [targetapidb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [targetapidb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [targetapidb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [targetapidb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [targetapidb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [targetapidb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [targetapidb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [targetapidb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [targetapidb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [targetapidb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [targetapidb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [targetapidb] SET RECOVERY FULL 
GO
ALTER DATABASE [targetapidb] SET  MULTI_USER 
GO
ALTER DATABASE [targetapidb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [targetapidb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [targetapidb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [targetapidb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [targetapidb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [targetapidb] SET QUERY_STORE = OFF
GO
USE [targetapidb]
GO
/****** Object:  Table [dbo].[ClienteModel]    Script Date: 25/10/2021 17:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClienteModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomeCompleto] [text] NOT NULL,
	[CPF] [varchar](255) NOT NULL,
	[DataNascimento] [date] NOT NULL,
	[EnderecoModel_Id] [int] NOT NULL,
	[FinanceiroModel_Id] [int] NOT NULL,
	[DateModificado] [datetime] NULL,
	[DateCadastro] [datetime] NOT NULL,
	[VipModel_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EnderecoModel]    Script Date: 25/10/2021 17:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnderecoModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Logradouro] [varchar](255) NOT NULL,
	[Bairro] [varchar](255) NOT NULL,
	[Cidade] [varchar](255) NOT NULL,
	[UF] [varchar](255) NOT NULL,
	[CEP] [varchar](255) NOT NULL,
	[Complemento] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinanceiroModel]    Script Date: 25/10/2021 17:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinanceiroModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RendaMensal] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VipModel]    Script Date: 25/10/2021 17:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VipModel](
	[Id] [int] NOT NULL,
	[Nome] [text] NOT NULL,
	[Preco] [money] NOT NULL,
	[Descricao] [text] NULL,
 CONSTRAINT [PK_VipModel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClienteModel] ADD  DEFAULT (getdate()) FOR [DateCadastro]
GO
ALTER TABLE [dbo].[ClienteModel]  WITH CHECK ADD  CONSTRAINT [FK_ClienteModel_EnderecoModel] FOREIGN KEY([EnderecoModel_Id])
REFERENCES [dbo].[EnderecoModel] ([Id])
GO
ALTER TABLE [dbo].[ClienteModel] CHECK CONSTRAINT [FK_ClienteModel_EnderecoModel]
GO
ALTER TABLE [dbo].[ClienteModel]  WITH CHECK ADD  CONSTRAINT [FK_ClienteModel_FinanceiroModel] FOREIGN KEY([FinanceiroModel_Id])
REFERENCES [dbo].[FinanceiroModel] ([Id])
GO
ALTER TABLE [dbo].[ClienteModel] CHECK CONSTRAINT [FK_ClienteModel_FinanceiroModel]
GO
ALTER TABLE [dbo].[ClienteModel]  WITH CHECK ADD  CONSTRAINT [FK_ClienteModel_VipModel] FOREIGN KEY([VipModel_Id])
REFERENCES [dbo].[VipModel] ([Id])
GO
ALTER TABLE [dbo].[ClienteModel] CHECK CONSTRAINT [FK_ClienteModel_VipModel]
GO
USE [master]
GO
ALTER DATABASE [targetapidb] SET  READ_WRITE 
GO
USE [targetapidb]
GO
INSERT INTO [dbo].[VipModel]
           ([Id]
           ,[Nome]
           ,[Preco]
           ,[Descricao])
     VALUES
           (1
           ,'Vip 1'
           ,50.00
           ,'Você ganhou um super robô para ajudar nos seus investimentos. Dica: Invista na YuriCorp')
GO

