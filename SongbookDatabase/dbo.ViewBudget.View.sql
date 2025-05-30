SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[ViewBudget]
AS

SELECT [ID], TrDate, Amount, AmountNormalized, TrType, Descrip, Account, TrCode, Balance, Comment, Descrip2, IsIncome, Ignore,
CONVERT(varchar,Trdate)+ ':' +FORMAT(ID, '00000000') AS OrderField,
DateWithMBOffset, DATEFROMPARTS(YEAR(DateWithMBOffset), MONTH(DateWithMBOffset), 1) AS MonthWithMBOffset FROM 
(SELECT        dbo.Budget.[ID], dbo.Budget.TrDate, dbo.Budget.Amount, CASE WHEN BudgetTypeGroupings.IsIncome = 1 THEN Amount ELSE -Amount END AS AmountNormalized, dbo.Budget.TrType, dbo.Budget.Descrip, dbo.Budget.Account, 
dbo.Budget.TrCode, DATEADD(day, 
                         dbo.BudgetConfig.MonthBoundaryOffset, dbo.Budget.TrDate) AS DateWithMBOffset, dbo.Budget.Balance, dbo.Budget.Comment, dbo.Budget.Descrip2, 
						 dbo.Budget.DescripFromVendor, BudgetTypeGroupings.IsIncome, dbo.Budget.Ignore
FROM            dbo.Budget INNER JOIN
                         dbo.BudgetConfig ON dbo.BudgetConfig.ID = 1 AND dbo.Budget.TrDate >= dbo.BudgetConfig.BaseDate
						 LEFT OUTER JOIN
                         BudgetTypeGroupings ON dbo.Budget.TrType = BudgetTypeGroupings.TrTypeID
						 ) AS InnerSelect

GO
