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

CREATE VIEW [songbook].[viewsongrecordings] (
   [Song], 
   [Comment], 
   [TitleAndArtist], 
   [PerformanceDate], 
   [VenueName])
AS 
   SELECT 
      viewsongperformances.Song AS Song, 
      viewsongperformances.Comment AS Comment, 
      viewsongperformances.TitleAndArtist AS TitleAndArtist, 
      viewsongperformances.PerformanceDate AS PerformanceDate, 
      viewsongperformances.VenueName AS VenueName
   FROM songbook.viewsongperformances
   WHERE (viewsongperformances.Comment LIKE N'%recording%')
GO
