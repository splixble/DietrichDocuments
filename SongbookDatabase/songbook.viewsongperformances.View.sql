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
CREATE VIEW [songbook].[viewsongperformances]
AS
SELECT        ISNULL(songperformances.ID, - 1) AS PerformanceID, songperformances.Song AS Song, songperformances.Comment, songperformances.SetNumber, performances.Comment AS PerfComment, viewsongssinglefield.TitleAndArtist, performances.PerformanceDate, performances.DidILead, 
                         YEAR(performances.PerformanceDate) AS PerformanceYear, CAST(YEAR(performances.PerformanceDate) AS varchar(50)) + N'/' + CAST((DATEPART(MONTH, performances.PerformanceDate) - 1) / 3 + 1 AS varchar(50)) 
                         AS PerformanceQtr, CAST(YEAR(performances.PerformanceDate) AS varchar(50)) + N'/' + CAST(DATEPART(MONTH, performances.PerformanceDate) AS varchar(50)) AS PerformanceMonth, venues.Name AS VenueName, 
                         venues.ID AS Venue, performances.ID AS PerfID, songperformances.ID AS SongPerfID
FROM            songbook.songperformances INNER JOIN
                         songbook.viewsongssinglefield ON songperformances.Song = viewsongssinglefield.ID INNER JOIN
                         songbook.performances ON songperformances.Performance = performances.ID INNER JOIN
                         songbook.venues ON performances.Venue = venues.ID
WHERE        (viewsongssinglefield.cover = 0)
GO
