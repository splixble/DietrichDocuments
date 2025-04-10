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

CREATE VIEW [songbook].[viewtocartist] (
   [ArtistAndTitle], 
   [FullTitle], 
   [FullArtistName], 
   [PageNumber])
AS 
   SELECT TOP (9223372036854775807) (viewsongs.FullArtistName + (': ' + viewsongs.FullTitle)) AS ArtistAndTitle, viewsongs.FullTitle AS FullTitle, viewsongs.FullArtistName AS FullArtistName, viewsongs.PageNumber AS PageNumber
   FROM songbook.viewsongs
   WHERE (viewsongs.PageNumber IS NOT NULL)
      ORDER BY 
         viewsongs.ArtistLastName, 
         viewsongs.ArtistFirstName, 
         viewsongs.Title, 
         viewsongs.TitlePrefix
GO
