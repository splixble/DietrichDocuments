USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE VIEW [dbo].[Old_ViewMonthsAndGroupings]
AS
SELECT        dbo.ViewBudgetAllMonths.MonthDate, dbo.ViewGroupings.GroupingKey, dbo.ViewGroupings.ParentKey AS GroupingParent, dbo.ViewGroupings.GroupingType, 
	AccountOwner.OwnerID AS AccountOwner, AccountType.TypeCode AS AccountType
FROM            dbo.ViewBudgetAllMonths CROSS JOIN
                         ViewGroupings
						 CROSS JOIN AccountOwner
						 CROSS JOIN AccountType
GO
