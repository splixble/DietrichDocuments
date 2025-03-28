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

CREATE VIEW [songbook].[viewsongperformancetotals] (
   [Total], 
   [SongID], 
   [TitleAndArtist], 
   [firstPerformed], 
   [lastPerformed], 
   RowNum)
AS 
SELECT Total, SongID, TitleAndArtist, firstPerformed, lastPerformed,
ROW_NUMBER() OVER (ORDER BY Total DESC, firstPerformed) AS RowNum
FROM 
   (SELECT TOP (9223372036854775807) 
      count_big(songperformances.ID) AS Total, 
      ISNULL(max(songperformances.Song), -1) AS SongID, /* for ASP MVC Entitie to infer this as Primary Key */
      max(viewsongssinglefield.TitleAndArtist) AS TitleAndArtist, 
      min(performances.PerformanceDate) AS firstPerformed, 
      max(performances.PerformanceDate) AS lastPerformed
   FROM ((songbook.songperformances 
      INNER JOIN songbook.viewsongssinglefield 
      ON ((songperformances.Song = viewsongssinglefield.ID))) 
      INNER JOIN songbook.performances 
      ON ((songperformances.Performance = performances.ID)))
   WHERE ((viewsongssinglefield.cover = 0) AND (performances.DidILead  = 1))
   GROUP BY songperformances.Song
      ORDER BY songperformances.Song)
	  AS InnerView
GO
