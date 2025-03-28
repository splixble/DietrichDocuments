SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[ViewBudgetWithMonthlySums]
AS
	SELECT        TOP (100) PERCENT SUM(AmountNormalized) AS AmountNormalized, MAX(Grouping) AS Grouping, MAX(GroupingParent) AS GroupingParent, MAX(MonthWithMBOffset) AS TrMonth
	FROM            dbo.ViewBudgetWithMonthly
	WHERE        (Grouping IS NOT NULL)
	GROUP BY Grouping, MonthWithMBOffset
UNION 
-- This adds a row with Amount=0 for every combination of Month and Grouping where there were no items, to avoid discontinuous lines on the chart:
SELECT 0 AS AmountNormalized, MG.[Grouping], MG.GroupingParent, MG.[MonthDate] AS TrMonth
  FROM [dbo].[ViewBudgetMonthsAndGroupings] MG
	FULL OUTER JOIN ViewBudgetWithMonthly BWM ON MG.Grouping = BWM.Grouping AND MG.MonthDate = BWM.MonthWithMBOffset
	WHERE BWM.Grouping IS NULL OR BWM.MonthWithMBOffset IS NULL
GO
