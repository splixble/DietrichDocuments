USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE VIEW [dbo].[ViewBudget]
AS

SELECT [ID], TrDate, Amount, AmountNormalized, TrType, [Grouping], GroupingParent, Descrip, Account, TrCode, Balance, Comment, Descrip2, IsIncome, Ignore, AccountOwner,
CONVERT(varchar,Trdate)+ ':' +FORMAT(ID, '00000000') AS OrderField,
DateWithMBOffset, DATEFROMPARTS(YEAR(DateWithMBOffset), MONTH(DateWithMBOffset), 1) AS MonthWithMBOffset FROM 
(SELECT        dbo.Budget.[ID], dbo.Budget.TrDate, dbo.Budget.Amount, CASE WHEN BudgetTypeGroupings.IsIncome = 1 THEN Amount ELSE -Amount END AS AmountNormalized, dbo.Budget.TrType, dbo.Budget.Descrip, dbo.Budget.Account, 
dbo.Budget.TrCode, DATEADD(day, 
                         dbo.BudgetConfig.MonthBoundaryOffset, dbo.Budget.TrDate) AS DateWithMBOffset, dbo.Budget.Balance, dbo.Budget.Comment, dbo.Budget.Descrip2, 
						 dbo.Budget.DescripFromVendor, BudgetTypeGroupings.IsIncome, 
						 ISNULL(BudgetTypeGroupings.GroupingLabel, TrType) AS [Grouping], BudgetTypeGroupings.ParentGroupingLabel AS GroupingParent, dbo.Budget.Ignore, BudgetAccount.AccountOwner
FROM            dbo.Budget INNER JOIN
                         dbo.BudgetConfig ON dbo.BudgetConfig.ID = 1 AND dbo.Budget.TrDate >= dbo.BudgetConfig.BaseDate
						 LEFT OUTER JOIN
                         BudgetTypeGroupings ON dbo.Budget.TrType = BudgetTypeGroupings.TrTypeID
						 LEFT OUTER JOIN
						 BudgetAccount ON Budget.Account = BudgetAccount.AccountID
						 ) AS InnerSelect

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Budget (dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 265
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "BudgetConfig (dbo)"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 102
               Right = 454
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewBudget'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewBudget'
GO
