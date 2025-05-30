USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceFileItems](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[SourceFile] [int] NOT NULL,
	[SourceFileLine] [int] NULL,
	[BudgetItem] [int] NULL,
	[SharePriceDate] [date] NULL,
	[SharePriceFund] [char](4) NULL,
 CONSTRAINT [PK_BudgetSourceFileItems] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SourceFileItems]  WITH CHECK ADD  CONSTRAINT [FK_BudgetSourceFileItems_BudgetSourceFile] FOREIGN KEY([SourceFile])
REFERENCES [dbo].[SourceFile] ([FileID])
GO
ALTER TABLE [dbo].[SourceFileItems] CHECK CONSTRAINT [FK_BudgetSourceFileItems_BudgetSourceFile]
GO
