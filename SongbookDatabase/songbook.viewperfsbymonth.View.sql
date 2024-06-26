SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
*   SSMA informational messages:
*   M2SS0003: The following SQL clause was ignored during conversion:
*   ALGORITHM =  UNDEFINED.
*   M2SS0003: The following SQL clause was ignored during conversion:
*   DEFINER = `root`@`localhost`.
*   M2SS0003: The following SQL clause was ignored during conversion:
*   SQL SECURITY DEFINER.
*/

CREATE VIEW [songbook].[viewperfsbymonth] ([Total], [max(`viewsongperformances`.`Performancemonth`)])
AS 
   SELECT TOP (9223372036854775807) count_big(viewsongperformances.Song) AS Total, max(viewsongperformances.PerformanceMonth) AS [max(`viewsongperformances`.`Performancemonth`)]
   FROM songbook.viewsongperformances
   GROUP BY viewsongperformances.PerformanceMonth
      ORDER BY viewsongperformances.PerformanceMonth
GO
