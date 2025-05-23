USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [char](4) NOT NULL,
	[AccountName] [varchar](80) NOT NULL,
	[SourceFileLocation] [varchar](250) NULL,
	[DefaultFormatAutoEntry] [varchar](50) NULL,
	[DefaultFormatManualEntry] [varchar](50) NULL,
	[AccountOwner] [char](4) NOT NULL,
	[CurrentlyTracked] [bit] NOT NULL,
	[TrackedByShares] [bit] NOT NULL,
	[AccountType] [char](1) NOT NULL,
	[Fund] [char](4) NULL,
	[Comment] [varchar](250) NULL,
	[AccountNumber] [varchar](50) NULL,
	[GraphColor] [varchar](50) NULL,
 CONSTRAINT [PK_BudgetAccount] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_BudgetAccount_AccountOwner] FOREIGN KEY([AccountOwner])
REFERENCES [dbo].[AccountOwner] ([OwnerID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_BudgetAccount_AccountOwner]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_BudgetAccount_AccountType] FOREIGN KEY([AccountType])
REFERENCES [dbo].[AccountType] ([TypeCode])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_BudgetAccount_AccountType]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_BudgetAccount_BudgetSourceFileFormat1] FOREIGN KEY([DefaultFormatAutoEntry])
REFERENCES [dbo].[SourceFileFormat] ([FormatCode])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_BudgetAccount_BudgetSourceFileFormat1]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_BudgetAccount_BudgetSourceFileFormat2] FOREIGN KEY([DefaultFormatManualEntry])
REFERENCES [dbo].[SourceFileFormat] ([FormatCode])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_BudgetAccount_BudgetSourceFileFormat2]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_BudgetAccount_Fund] FOREIGN KEY([Fund])
REFERENCES [dbo].[Fund] ([FundID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_BudgetAccount_Fund]
GO
