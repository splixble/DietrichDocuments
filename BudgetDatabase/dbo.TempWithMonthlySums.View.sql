USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[TempWithMonthlySums]
AS
SELECT 
    MS.AmountNormalized,
	MG.Grouping,
	MG.GroupingParent, 
	MG.[MonthDate] AS TrMonth, 
	MG.AccountOwner, 
	MG.AccountType FROM
(SELECT        TOP (100) PERCENT SUM(AmountNormalized) AS AmountNormalized, MAX(Grouping) AS Grouping, MAX(GroupingParent) AS GroupingParent, MAX(MonthWithMBOffset) AS TrMonth, 
    MAX(AccountOwner) As AccountOwner, MAX(AccountType) As AccountType
	FROM            dbo.ViewBudgetWithMonthly 
	WHERE        (Grouping IS NOT NULL)
	GROUP BY Grouping, MonthWithMBOffset, AccountOwner, AccountType
) AS MS
-- This adds a row with Amount=0 for every combination of Month, Grouping, AccountOwner, and AccountType where there were no items, to avoid discontinuous lines on the chart:
RIGHT OUTER JOIN [ViewBudgetMonthsAndGroupings] MG
  ON MS.Grouping = MG.Grouping AND ms.TrMonth = MG.MonthDate AND MG.AccountOwner = MS.AccountOwner AND MG.AccountType = MS.AccountType
	


GO
