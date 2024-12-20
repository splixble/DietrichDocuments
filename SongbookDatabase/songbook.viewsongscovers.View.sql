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

CREATE VIEW [songbook].[viewsongscovers] ([SongID], [ArtistID], [Cover])
AS 
   SELECT songs.ID AS SongID, songs.Artist AS ArtistID, 0 AS Cover
   FROM songbook.songs
    UNION
   SELECT alternateartists.SongID AS SongID, alternateartists.ArtistID AS ArtistID, 1 AS Cover
   FROM songbook.alternateartists
GO
