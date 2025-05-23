USE [Budget]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE VIEW [dbo].[ViewBudgetMonthlyBalancesPerAccount]
AS
SELECT        ViewBalances.OrderField, ViewBalances.TrDate, ViewBalances.Account, BalanceMonth AS MonthWithMBOffset, ViewBalances.Balance, Account.AccountOwner
FROM            dbo.ViewBalances INNER JOIN (
-- For each month in the history of the data, get the last balance before the end of the month for a given account -- even if the balance is from a previous month:
select MAX(Months.MonthDate) AS BalanceMonth, MAX(ViewBalances_1.Account) AS Account1, MAX(OrderField) AS OrderField1
from [MonthsInDateRange]((Select BaseDate from BudgetConfig), GETDATE()) As Months
inner join [ViewBalances] AS ViewBalances_1 
on Months.MonthDate >= ViewBalances_1.MonthWithMBOffset 
AND (ViewBalances_1.Balance IS NOT NULL)
GROUP BY ViewBalances_1.Account, Months.MonthDate
) AS MonthlyMaxes ON ViewBalances.Account = MonthlyMaxes.Account1 AND 
ViewBalances.OrderField = MonthlyMaxes.OrderField1 
-- NOTE: Changed previous line on 15Nov24 to ViewBalances.MonthWithMBOffset = MonthlyMaxes.BalanceMonth for performance, but that returned multiple rows per month/acct, so reverted 27Nov24 
INNER JOIN Account ON ViewBalances.Account = Account.AccountID
-- was, before 23Aug24:
-- SELECT        ViewBudget.ID, ViewBudget.OrderField, ViewBudget.TrDate, ViewBudget.Account, ViewBudget.MonthWithMBOffset, ViewBudget.Balance
-- FROM            dbo.ViewBudget INNER JOIN
--                              (SELECT        MAX(OrderField) AS OrderField1, MAX(MonthWithMBOffset) AS Month1, MAX(Account) AS Account1
--                                FROM            dbo.ViewBudget AS ViewBudget_1
--                                WHERE        (Balance IS NOT NULL)
--                                GROUP BY Account, MonthWithMBOffset) AS MonthlyMaxes ON ViewBudget.OrderField = MonthlyMaxes.OrderField1
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
         Begin Table = "ViewBudget (dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MonthlyMaxes"
            Begin Extent = 
               Top = 6
               Left = 276
               Bottom = 119
               Right = 446
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewBudgetMonthlyBalancesPerAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewBudgetMonthlyBalancesPerAccount'
GO
