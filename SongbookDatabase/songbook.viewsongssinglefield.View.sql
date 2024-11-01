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

CREATE VIEW [songbook].[viewsongssinglefield] (
   [SongFull], 
   [TitleAndInfo], 
   [TitleAndArtist], 
   [FullTitle],
   [SongFullArtistFirst], 
   [SongInfo], 
   [ArtistFirstName], 
   [ArtistLastName], 
   [FullArtistName], 
   [Title], 
   [TitlePrefix], 
   [ID], 
   [Code], 
   [PageNumber], 
   [InTablet], 
   [cover])
AS 
   /*
   *   SSMA warning messages:
   *   M2SS0242: String to String conversion in CONVERT statement is ignored
   *   M2SS0242: String to String conversion in CONVERT statement is ignored
   *   M2SS0044: Type 'char(500) CHARACTER SET utf8mb3' was not converted because there is no mapping for it. Add a mapping and then convert again.
   *   M2SS0242: String to String conversion in CONVERT statement is ignored
   *   M2SS0044: Type 'char(500) CHARACTER SET utf8mb3' was not converted because there is no mapping for it. Add a mapping and then convert again.
   *   M2SS0044: Type 'char(500) CHARACTER SET utf8mb3' was not converted because there is no mapping for it. Add a mapping and then convert again.
   *   M2SS0242: String to String conversion in CONVERT statement is ignored
   *   M2SS0242: String to String conversion in CONVERT statement is ignored
   *   M2SS0044: Type 'char(500) CHARACTER SET utf8mb3' was not converted because there is no mapping for it. Add a mapping and then convert again.
   */

   SELECT 
      viewsongs.FullTitle + N' (' + viewsongs.FullArtistName + N')' + viewsongs.SongInfo AS SongFull, 
      viewsongs.FullTitle + viewsongs.SongInfo AS TitleAndInfo, 
      viewsongs.FullTitle + ' (' + viewsongs.FullArtistName + ')' AS TitleAndArtist, 
	  viewsongs.FullTitle AS FullTitle,
      viewsongs.FullArtistName + N': ' + viewsongs.FullTitle + viewsongs.SongInfo AS SongFullArtistFirst, 
      viewsongs.SongInfo AS SongInfo, 
      viewsongs.ArtistFirstName AS ArtistFirstName, 
      viewsongs.ArtistLastName AS ArtistLastName, 
      viewsongs.FullArtistName AS FullArtistName, 
      viewsongs.Title AS Title, 
      viewsongs.TitlePrefix AS TitlePrefix, 
      ISNULL(viewsongs.ID, -1) AS ID, 
      viewsongs.Code AS Code, 
      viewsongs.PageNumber AS PageNumber, 
      viewsongs.InTablet AS InTablet, 
      viewsongs.Cover AS cover
   FROM songbook.viewsongs
   WHERE (viewsongs.Cover = 0)
GO
