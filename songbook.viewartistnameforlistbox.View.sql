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

CREATE VIEW [songbook].[viewartistnameforlistbox] (
   [Name], 
   [ArtistLastName], 
   [Artist], 
   [ArtistFirstName])
AS 
   SELECT TOP (9223372036854775807) ltrim(ISNULL(artists.ArtistFirstName, '') + ' ' + ISNULL(artists.ArtistLastName, '')) AS Name, artists.ArtistLastName AS ArtistLastName, artists.ArtistID AS Artist, artists.ArtistFirstName AS ArtistFirstName
   FROM songbook.artists
      ORDER BY artists.ArtistFirstName, artists.ArtistLastName
GO
