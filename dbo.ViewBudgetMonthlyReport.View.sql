SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[ViewBudgetMonthlyReport]
AS
SELECT TOP 100 PERCENT AmountNormalized, Grouping, GroupingParent, IIF(GroupingParent IS NULL, Grouping, GroupingParent + ':' + Grouping) AS GroupingWithParent, TrMonth,
Grouping + '-' + CONVERT(varchar, TrMonth, 1) AS ViewKey
FROM            ViewBudgetWithMonthlySums
ORDER BY TrMonth, GroupingWithParent
GO
