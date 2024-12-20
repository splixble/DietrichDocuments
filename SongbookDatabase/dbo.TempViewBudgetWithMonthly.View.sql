SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* where [AmountNormalized] <> AmountGroupized AND GroupingType <> 'L' and GroupingType <> 'A'*/
CREATE VIEW [dbo].[TempViewBudgetWithMonthly]
AS
SELECT        TrDate, TrType, Grouping, GroupingParent, GroupingType, Descrip, Account, TrCode, Amount, AmountNormalized, AmountGroupized
FROM            (SELECT        TOP (10000) TrDate, TrType, Grouping, GroupingParent, GroupingType, Descrip, Account, TrCode, Amount, AmountNormalized, CASE WHEN Grouping = 'Income' OR
                                                    GroupingParent = 'Income' THEN Amount ELSE - Amount END AS AmountGroupized
                          FROM            dbo.ViewBudgetWithMonthly) AS InnerQ
GO
