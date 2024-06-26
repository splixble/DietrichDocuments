SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewBudgetAllMonths]
AS
Select MonthDate FROM BudgetConfig C
CROSS Apply MonthsInDateRange(DATEFROMPARTS(YEAR(C.BaseDate), MONTH(C.BaseDate), 1), GETDATE())
WHERE C.ID = 1
GO
