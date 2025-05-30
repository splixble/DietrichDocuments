USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OldBudgetTypeGroupings](
	[TRTypeID] [char](4) NOT NULL,
	[GroupingLabel] [varchar](250) NOT NULL,
	[ParentGroupingLabel] [varchar](250) NULL,
	[CodeAndName]  AS ((Trim([TrTypeID])+' ')+[GroupingLabel]) PERSISTED NOT NULL,
	[IsIncome]  AS (case when [ParentGroupingLabel]='Income' then (1) else (0) end) PERSISTED NOT NULL
) ON [PRIMARY]
GO
