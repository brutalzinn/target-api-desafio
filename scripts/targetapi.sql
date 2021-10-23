USE [targetapidb]
GO
/****** Object:  Table [dbo].[ClienteModel]    Script Date: 23/10/2021 15:53:37 ******/
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
/****** Object:  Table [dbo].[EnderecoModel]    Script Date: 23/10/2021 15:53:37 ******/
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
/****** Object:  Table [dbo].[FinanceiroModel]    Script Date: 23/10/2021 15:53:37 ******/
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
/****** Object:  Table [dbo].[VipModel]    Script Date: 23/10/2021 15:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VipModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [text] NOT NULL,
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
