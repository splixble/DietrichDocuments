SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetAccount](
	[AccountID] [char](4) NOT NULL,
	[AccountName] [varchar](80) NOT NULL,
	[SourceFileLocation] [varchar](250) NULL,
	[SourceFileFormat] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BudgetAccount] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BudgetAccount]  WITH CHECK ADD  CONSTRAINT [FK_BudgetAccount_BudgetSourceFileFormat] FOREIGN KEY([SourceFileFormat])
REFERENCES [dbo].[BudgetSourceFileFormat] ([FormatCode])
GO
ALTER TABLE [dbo].[BudgetAccount] CHECK CONSTRAINT [FK_BudgetAccount_BudgetSourceFileFormat]
GO
