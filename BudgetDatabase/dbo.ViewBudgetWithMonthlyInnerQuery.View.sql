USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[ViewBudgetWithMonthlyInnerQuery]
AS
/* Basic types:*/ SELECT TrDate, Amount, AmountNormalized, TrType, [Grouping] AS [Grouping],  GroupingParent AS GroupingParent, 
						IIF(GroupingParent IS NULL, Grouping, GroupingParent + ':' + Grouping) AS GroupingWithParent,
						 'B' AS GroupingType, Descrip, Account, TrCode, MonthWithMBOffset
FROM            dbo.ViewBudget 
						 WHERE (Ignore = 0)
UNION
/* Parent (consolidated) types:*/ SELECT TrDate, Amount, AmountNormalized, TrType, GroupingParent AS [Grouping], NULL AS [GroupingParent], 
						GroupingParent AS GroupingWithParent,
                         'P' AS GroupingType, Descrip, Account, TrCode, MonthWithMBOffset
FROM            dbo.ViewBudget 
						 WHERE (Ignore = 0)
UNION
/* Total expenditures:*/ SELECT TrDate, Amount, AmountNormalized, TrType, 'Expenses' AS [Grouping], NULL AS [GroupingParent],
			'Expenditures' AS GroupingWithParent,
			'E' AS GroupingType, Descrip, Account, TrCode, MonthWithMBOffset
	FROM dbo.ViewBudget
	WHERE dbo.ViewBudget.IsIncome = 0 AND dbo.ViewBudget.Ignore = 0
UNION
	/* Monthly balance per account */
	SELECT TrDate, 0 As Amount, Balance AS AmountNormalized, NULL As TrType, 'Balance, ' + Account.AccountName as [Grouping], 'Balance' AS [GroupingParent],
	'Balance.' + Account.AccountName as GroupingWithParent,
	'A' AS GroupingType, '' AS Descrip, Account, NULL AS TrCode, MonthWithMBOffset
	FROM [dbo].[ViewBudgetMonthlyBalancesPerAccount]
	INNER JOIN Account ON  [ViewBudgetMonthlyBalancesPerAccount].Account = Account.AccountID
UNION
	/* Monthly balance total */
	SELECT TrDate, 0 As Amount, Balance AS AmountNormalized, NULL As TrType, 'Balance' AS [Grouping], NULL AS [GroupingParent],
	'Balance' as GroupingWithParent,
	'L' AS GroupingType, '' AS Descrip, Account, NULL AS TrCode, MonthWithMBOffset
	FROM [dbo].[ViewBudgetMonthlyBalancesPerAccount]
GO
