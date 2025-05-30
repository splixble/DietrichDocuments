SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE VIEW [dbo].[ViewBudgetWithMonthly]
AS
/* Basic types:*/ SELECT TrDate, Amount, AmountNormalized, TrType, ISNULL(BudgetTypeGroupings.GroupingLabel, TrType) AS [Grouping],  BudgetTypeGroupings.ParentGroupingLabel AS [GroupingParent],
						 'B' AS GroupingType, Descrip, Account, TrCode, DateWithMBOffset, MonthWithMBOffset
FROM            dbo.ViewBudget LEFT OUTER JOIN
                         BudgetTypeGroupings ON ViewBudget.TrType = BudgetTypeGroupings.TrTypeID
						 WHERE (dbo.ViewBudget.Ignore = 0)
UNION
/* Parent (consolidated) types:*/ SELECT TrDate, Amount, AmountNormalized, TrType, BudgetTypeGroupings.ParentGroupingLabel AS [Grouping], NULL AS [GroupingParent], 
                         'P' AS GroupingType, Descrip, Account, TrCode, DateWithMBOffset, MonthWithMBOffset
FROM            dbo.ViewBudget INNER JOIN
                         BudgetTypeGroupings ON ViewBudget.TrType = BudgetTypeGroupings.TrTypeID AND BudgetTypeGroupings.ParentGroupingLabel IS NOT NULL
						 WHERE (dbo.ViewBudget.Ignore = 0)
UNION
/* Total expenditures:*/ SELECT TrDate, Amount, AmountNormalized, TrType, 'Expenditures' AS [Grouping], NULL AS [GroupingParent],
			'E' AS GroupingType, Descrip, Account, TrCode, DateWithMBOffset, MonthWithMBOffset
	FROM dbo.ViewBudget
	WHERE dbo.ViewBudget.IsIncome = 0 AND dbo.ViewBudget.Ignore = 0
UNION
	/* Monthly balance per account */
	SELECT TrDate, 0 As Amount, Balance AS AmountNormalized, NULL As TrType, 'Balance, ' + BudgetAccount.AccountName as [Grouping], 'Balance' AS [GroupingParent],
	'A' AS GroupingType, '' AS Descrip, Account, NULL AS TrCode, NULL As DateWithMBOffset, MonthWithMBOffset
	FROM [dbo].[ViewBudgetMonthlyBalancesPerAccount]
	INNER JOIN BudgetAccount ON  [ViewBudgetMonthlyBalancesPerAccount].Account = BudgetAccount.AccountID
UNION
	/* Monthly balance total */
	SELECT TrDate, 0 As Amount, Balance AS AmountNormalized, NULL As TrType, 'Balance' [Grouping], NULL AS [GroupingParent],
	'L' AS GroupingType, '' AS Descrip, Account, NULL AS TrCode, NULL As DateWithMBOffset, MonthWithMBOffset
	FROM [dbo].[ViewBudgetMonthlyBalancesPerAccount]
	
GO
