USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Budget](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrDate] [date] NOT NULL,
	[Descrip] [varchar](500) NOT NULL,
	[Account] [char](4) NOT NULL,
	[TrType] [char](8) NULL,
	[TrCode] [varchar](250) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Ignore] [bit] NOT NULL,
	[Balance] [decimal](18, 2) NULL,
	[Comment] [varchar](max) NULL,
	[Descrip2] [varchar](500) NULL,
	[BalanceIsCalculated] [bit] NOT NULL,
	[DescripFromVendor] [varchar](500) NULL,
	[CardTransDate] [date] NULL,
 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_Budget_Account] ON [dbo].[Budget]
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_Budget_TrType] ON [dbo].[Budget]
(
	[TrType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TrDate] ON [dbo].[Budget]
(
	[TrDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Budget] ADD  CONSTRAINT [DF_Budget_Ignore]  DEFAULT ((0)) FOR [Ignore]
GO
ALTER TABLE [dbo].[Budget] ADD  CONSTRAINT [DF_Budget_BalanceIsCalculated]  DEFAULT ((0)) FOR [BalanceIsCalculated]
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_BudgetTypeGroupings] FOREIGN KEY([TrType])
REFERENCES [dbo].[BudgetTypeGroupings] ([TRTypeID])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_BudgetTypeGroupings]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Credit cards transactions are listed by Post Date, since we don''t always have a Transaction Date -- but if we do, it;s here.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Budget', @level2type=N'COLUMN',@level2name=N'CardTransDate'
GO
