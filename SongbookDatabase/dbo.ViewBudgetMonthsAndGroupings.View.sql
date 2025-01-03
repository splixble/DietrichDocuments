SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[ViewBudgetMonthsAndGroupings]
AS
SELECT        dbo.ViewBudgetAllMonths.MonthDate, dbo.BudgetTypeGroupings.GroupingLabel AS [Grouping], dbo.BudgetTypeGroupings.ParentGroupingLabel AS GroupingParent
FROM            dbo.ViewBudgetAllMonths CROSS JOIN
                         dbo.BudgetTypeGroupings
GO
