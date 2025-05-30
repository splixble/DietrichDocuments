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

CREATE VIEW [songbook].[viewtoctitle] ([TitleAndArtist], [PageNumber])
AS 
   SELECT TOP (9223372036854775807) (viewsongs.FullTitle + ' (') + (viewsongs.FullArtistName + ')') AS TitleAndArtist, viewsongs.PageNumber AS PageNumber
   FROM songbook.viewsongs
   WHERE ((viewsongs.PageNumber IS NOT NULL) AND (viewsongs.Cover = 0))
      ORDER BY 
         viewsongs.Title, 
         viewsongs.TitlePrefix, 
         viewsongs.ArtistLastName, 
         viewsongs.ArtistFirstName
GO
