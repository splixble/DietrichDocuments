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

CREATE VIEW [songbook].[viewperftotalsbyyear] (
   [Total], 
   [SongID], 
   [max(`viewsongperformances`.`PerformanceYear`)], 
   [max(`viewsongperformances`.TitleAndArtist )])
AS 
   SELECT TOP (9223372036854775807) count_big(viewsongperformances.Song) AS Total, max(viewsongperformances.Song) AS SongID, max(viewsongperformances.PerformanceYear) AS [max(`viewsongperformances`.`PerformanceYear`)], max(viewsongperformances.TitleAndArtist) AS [max(`viewsongperformances`.TitleAndArtist )]
   FROM songbook.viewsongperformances
   GROUP BY viewsongperformances.PerformanceYear, viewsongperformances.Song
      ORDER BY viewsongperformances.PerformanceYear, count_big(viewsongperformances.Song) DESC, viewsongperformances.Song
GO
