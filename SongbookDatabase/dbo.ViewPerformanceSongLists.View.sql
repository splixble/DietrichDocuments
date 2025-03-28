SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewPerformanceSongLists]
AS
SELECT MAX(songbook.performances.ID) AS PerfID,  MAX(songbook.performances.PerformanceDate) AS PerfDate, MAX(songbook.venues.Name) AS VenueName, -- MAX(songbook.performances.Comment),
STRING_AGG(songbook.viewsongssinglefield.TitleAndArtist, '~ ') AS SongList
FROM   
songbook.songperformances INNER JOIN
 songbook.performances ON songbook.songperformances.Performance = songbook.performances.ID INNER JOIN
             songbook.viewsongssinglefield ON songbook.viewsongssinglefield.ID = songbook.songperformances.Song INNER JOIN
             songbook.venues ON songbook.performances.Venue = songbook.venues.ID
			 GROUP BY  songbook.songperformances.Performance
GO
