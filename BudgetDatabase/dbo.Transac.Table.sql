USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transac](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrDate] [date] NOT NULL,
	[Descrip] [varchar](500) NULL,
	[Account] [char](4) NOT NULL,
	[TrType] [char](8) NULL,
	[TrCode] [varchar](250) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Balance] [decimal](18, 2) NULL,
	[Comment] [varchar](max) NULL,
	[Descrip2] [varchar](500) NULL,
	[BalanceIsCalculated] [bit] NOT NULL,
	[DescripFromVendor] [varchar](500) NULL,
	[CardTransDate] [date] NULL,
	[AcctTransfer] [bit] NOT NULL,
 CONSTRAINT [PK_Transact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_Transac_Account_TrDate] ON [dbo].[Transac]
(
	[Account] ASC,
	[TrDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_Transac_TrDate_Account] ON [dbo].[Transac]
(
	[TrDate] ASC,
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IX_Transac_TrType_Account_Date] ON [dbo].[Transac]
(
	[TrType] ASC,
	[Account] ASC,
	[TrDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transac] ADD  CONSTRAINT [DF_Budget_BalanceIsCalculated]  DEFAULT ((0)) FOR [BalanceIsCalculated]
GO
ALTER TABLE [dbo].[Transac] ADD  CONSTRAINT [DF_Budget_AcctTransfer]  DEFAULT ((0)) FOR [AcctTransfer]
GO
ALTER TABLE [dbo].[Transac]  WITH CHECK ADD  CONSTRAINT [FK_Budget_BudgetTypeGroupings] FOREIGN KEY([TrType])
REFERENCES [dbo].[TransacType] ([TRTypeID])
GO
ALTER TABLE [dbo].[Transac] CHECK CONSTRAINT [FK_Budget_BudgetTypeGroupings]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Credit cards transactions are listed by Post Date, since we don''t always have a Transaction Date -- but if we do, it;s here.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Transac', @level2type=N'COLUMN',@level2name=N'CardTransDate'
GO
