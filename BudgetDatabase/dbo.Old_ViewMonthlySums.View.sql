USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








create VIEW [dbo].[Old_ViewMonthlySums]
AS
SELECT        TOP (100) PERCENT SUM(AmountNormalized) AS AmountNormalized, MAX(GroupingKey) AS GroupingKey, MAX(MonthWithMBOffset) AS TrMonth, 
    MAX(AccountOwner) As AccountOwner, MAX(AccountType) As AccountType
	FROM            dbo.ViewBudgetWithMonthly 
	WHERE        (GroupingKey IS NOT NULL)
	GROUP BY GroupingKey, MonthWithMBOffset, AccountOwner, AccountType


GO
