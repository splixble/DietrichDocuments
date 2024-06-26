SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewBudgetMonthlyBalances]
AS
SELECT        SUM(Balance) AS BalanceTotal, MAX(MonthWithMBOffset) AS MonthWithMBOffset
FROM            dbo.ViewBudgetMonthlyBalancesPerAccount
GROUP BY MonthWithMBOffset
GO
