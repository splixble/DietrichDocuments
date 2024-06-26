SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewBudgetMonthlyBalancesPerAccount]
AS
SELECT        ViewBudget.ID, ViewBudget.OrderField, ViewBudget.TrDate, ViewBudget.Account, ViewBudget.MonthWithMBOffset, ViewBudget.Balance
FROM            dbo.ViewBudget INNER JOIN
                             (SELECT        MAX(OrderField) AS OrderField1, MAX(MonthWithMBOffset) AS Month1, MAX(Account) AS Account1
                               FROM            dbo.ViewBudget AS ViewBudget_1
                               WHERE        (Balance IS NOT NULL)
                               GROUP BY Account, MonthWithMBOffset) AS MonthlyMaxes ON ViewBudget.OrderField = MonthlyMaxes.OrderField1
GO
