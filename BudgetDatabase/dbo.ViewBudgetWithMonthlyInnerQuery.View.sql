USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[ViewBudgetWithMonthlyInnerQuery]
AS
/* Transactions */ 
SELECT TrDate, Amount, AmountNormalized, TrType, VC.ContainingKey AS GroupingKey, 
						 VC.GroupingType, Descrip, Account, TrCode, MonthWithMBOffset 					 
FROM dbo.ViewBudget VB
    INNER JOIN ViewContainingGroupings VC on VB.TrType = VC.Subkey AND VC.GroupingType = 'T'
	WHERE (VB.AcctTransfer = 0)
						 -- and trdate = '2025-01-04' order by VB.Descrip
UNION

/* Total expenditures */ 
SELECT TrDate, Amount, AmountNormalized, TrType, VC.GroupingKey, 
						 VC.GroupingType AS GroupingType, Descrip, Account, TrCode, MonthWithMBOffset
	FROM dbo.ViewBudget VB
    INNER JOIN ViewContainingGroupings VC on VC.GroupingType = 'E'
    WHERE VB.IsIncome = 0 AND VB.AcctTransfer = 0 
	AND Amount IS NOT NULL -- don't include transactions with balance but no amount
UNION 

/* Monthly balance per account */
SELECT TrDate, 0 As Amount, Balance AS AmountNormalized, NULL As TrType, VC.ContainingKey AS GroupingKey, 
						 VC.GroupingType, '' AS Descrip, Account, NULL AS TrCode, MonthWithMBOffset
	FROM [dbo].[ViewBudgetMonthlyBalancesPerAccount] VB
	INNER JOIN ViewContainingGroupings VC on VB.Account = VC.Subkey AND VC.GroupingType = 'B'
GO
