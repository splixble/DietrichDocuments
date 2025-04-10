USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransacType](
	[TRTypeID] [char](8) NOT NULL,
	[GroupingLabel] [varchar](250) NOT NULL,
	[CodeAndName]  AS ((Trim([TrTypeID])+' ')+[GroupingLabel]) PERSISTED NOT NULL,
	[ParentGroupingID] [char](8) NULL,
	[IsParentGrouping] [bit] NOT NULL,
	[GraphColor] [varchar](50) NULL,
	[IsIncome]  AS (case when [TRTypeID]='IN_' OR [ParentGroupingID]='IN_' then (1) else (0) end),
 CONSTRAINT [PK_BudgetTypeGroupings] PRIMARY KEY CLUSTERED 
(
	[TRTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TransacType] ADD  CONSTRAINT [DF_BudgetTypeGroupings_IsParentGrouping]  DEFAULT ((0)) FOR [IsParentGrouping]
GO
ALTER TABLE [dbo].[TransacType]  WITH CHECK ADD  CONSTRAINT [FK_BudgetTypeGroupings_BudgetTypeGroupings] FOREIGN KEY([ParentGroupingID])
REFERENCES [dbo].[TransacType] ([TRTypeID])
GO
ALTER TABLE [dbo].[TransacType] CHECK CONSTRAINT [FK_BudgetTypeGroupings_BudgetTypeGroupings]
GO
