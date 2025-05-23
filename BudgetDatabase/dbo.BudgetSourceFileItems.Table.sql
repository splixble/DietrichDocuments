USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetSourceFileItems](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[SourceFile] [int] NOT NULL,
	[SourceFileLine] [int] NULL,
	[BudgetItem] [int] NOT NULL,
 CONSTRAINT [PK_BudgetSourceFileItems] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BudgetSourceFileItems]  WITH CHECK ADD  CONSTRAINT [FK_BudgetSourceFileItems_BudgetSourceFile] FOREIGN KEY([SourceFile])
REFERENCES [dbo].[BudgetSourceFile] ([FileID])
GO
ALTER TABLE [dbo].[BudgetSourceFileItems] CHECK CONSTRAINT [FK_BudgetSourceFileItems_BudgetSourceFile]
GO
