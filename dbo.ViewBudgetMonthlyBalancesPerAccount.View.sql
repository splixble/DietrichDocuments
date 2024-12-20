SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[ViewBudgetMonthlyBalancesPerAccount]
AS
SELECT        ViewBudget.ID, ViewBudget.OrderField, ViewBudget.TrDate, ViewBudget.Account, BalanceMonth AS MonthWithMBOffset, ViewBudget.Balance
FROM            dbo.ViewBudget INNER JOIN (
-- For each month in the history of the data, get the last balance before the end of the month for a given account -- even if the balance is from a previous month:
select MAX(Months.MonthDate) AS BalanceMonth, MAX(ViewBudget_1.Account) AS Account1, MAX(OrderField) AS OrderField1
from [MonthsInDateRange]((Select BaseDate from BudgetConfig), GETDATE()) As Months
inner join [ViewBudget] AS ViewBudget_1 
on Months.MonthDate >= ViewBudget_1.MonthWithMBOffset 
AND (ViewBudget_1.Balance IS NOT NULL)
GROUP BY ViewBudget_1.Account, Months.MonthDate
) AS MonthlyMaxes ON ViewBudget.OrderField = MonthlyMaxes.OrderField1
-- was, before 23Aug24:
-- SELECT        ViewBudget.ID, ViewBudget.OrderField, ViewBudget.TrDate, ViewBudget.Account, ViewBudget.MonthWithMBOffset, ViewBudget.Balance
-- FROM            dbo.ViewBudget INNER JOIN
--                              (SELECT        MAX(OrderField) AS OrderField1, MAX(MonthWithMBOffset) AS Month1, MAX(Account) AS Account1
--                                FROM            dbo.ViewBudget AS ViewBudget_1
--                                WHERE        (Balance IS NOT NULL)
--                                GROUP BY Account, MonthWithMBOffset) AS MonthlyMaxes ON ViewBudget.OrderField = MonthlyMaxes.OrderField1
GO
