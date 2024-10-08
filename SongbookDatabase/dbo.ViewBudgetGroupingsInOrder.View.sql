SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[ViewBudgetGroupingsInOrder]
AS
SELECT DISTINCT TOP (100) PERCENT Grouping, CASE [grouping] WHEN 'Income' THEN 1 WHEN 'Expenditures' THEN 2 ELSE 3 END AS OrderNum, 
GroupingType, dbo.BudgetTypeGroupings.ParentGroupingLabel
FROM            dbo.ViewBudgetWithMonthly
left join dbo.BudgetTypeGroupings on dbo.ViewBudgetWithMonthly.Grouping = dbo.BudgetTypeGroupings.GroupingLabel
WHERE        (Grouping IS NOT NULL)
ORDER BY OrderNum, Grouping
GO
