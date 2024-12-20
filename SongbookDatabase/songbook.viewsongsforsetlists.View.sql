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

CREATE VIEW [songbook].[viewsongsforsetlists] (
   [ID], 
   [FullTitle], 
   [FullArtistName], 
   [DiffPDFName], 
   [SetlistAddable], 
   [InTablet], 
   [SetlistCaption],
   [ArtistList])
AS 
   /*
   *   SSMA warning messages:
   *   M2SS0243: NULL to String conversion in CONVERT statement is ignored
   *   M2SS0044: Type 'char(500) CHARACTER SET utf8mb3' was not converted because there is no mapping for it. Add a mapping and then convert again.
   */

   SELECT 
      viewsongs.ID AS ID, 
      viewsongs.FullTitle AS FullTitle, 
      viewsongs.FullArtistName AS FullArtistName, 
      viewsongs.DiffPDFName AS DiffPDFName, 
      viewsongs.SetlistAddable AS SetlistAddable, 
      viewsongs.InTablet AS InTablet, 
      CAST(trim(ISNULL(viewsongs.FullArtistName, N'') + ISNULL(
         CASE 
            WHEN (CAST(viewsongs.InTablet AS bigint) <> 0) THEN N''
            ELSE N' (no lyrics)'
         END, N'') + ISNULL(' - In ' + viewsongs.songkey, '') + ISNULL(', orig. ' + viewsongs.OriginalKey, '') + ISNULL(', * ' + viewsongs.Comment, '')) AS varchar(8000)) AS SetlistCaption,
	 Art.ArtistsWithVirgules AS ArtistList
   FROM songbook.viewsongs
   INNER JOIN ViewSongsArtistLists Art ON viewsongs.ID = Art.ID  
   WHERE (((CAST(viewsongs.SetlistAddable AS bigint) = 1) OR (CAST(viewsongs.InTablet AS bigint) = 1)) AND (viewsongs.Cover = 0))
GO
