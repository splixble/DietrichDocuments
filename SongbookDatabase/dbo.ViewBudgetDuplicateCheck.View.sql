SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewBudgetDuplicateCheck]
AS
SELECT        COUNT(ID) AS cn, MAX(TrDate) AS Dat, MAX(Descrip) AS Des, MAX(TrCode) AS Cod, MAX(Amount) AS Amt
FROM            dbo.Budget
GROUP BY TrDate, Descrip, TrCode, Amount
GO
