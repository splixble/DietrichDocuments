SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetTypeGroupings](
	[TRTypeID] [char](4) NOT NULL,
	[GroupingLabel] [varchar](250) NOT NULL,
	[ParentGroupingLabel] [varchar](250) NULL,
	[CodeAndName]  AS ((Trim([TrTypeID])+' ')+[GroupingLabel]) PERSISTED NOT NULL,
	[IsIncome]  AS (case when [ParentGroupingLabel]='Income' then (1) else (0) end) PERSISTED NOT NULL,
 CONSTRAINT [PK_BudgetTypeGroupings] PRIMARY KEY CLUSTERED 
(
	[TRTypeID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
