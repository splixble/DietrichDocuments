USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetSourceFileFormat](
	[FormatCode] [varchar](50) NOT NULL,
	[FormatColumns] [varchar](250) NULL,
	[CreditsAreNegative] [bit] NOT NULL,
	[FileExtension] [varchar](12) NULL,
 CONSTRAINT [PK_BudgetSourceFileFormat] PRIMARY KEY CLUSTERED 
(
	[FormatCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
