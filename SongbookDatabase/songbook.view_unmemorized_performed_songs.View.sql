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

CREATE VIEW [songbook].[view_unmemorized_performed_songs] (
   [TitlePrefix], 
   [Title], 
   [Code], 
   [ID], 
   [ArtistFirstName])
AS 
   SELECT 
      viewsongs.TitlePrefix AS TitlePrefix, 
      viewsongs.Title AS Title, 
      viewsongs.Code AS Code, 
      viewsongs.ID AS ID, 
      viewsongs.ArtistFirstName AS ArtistFirstName
   FROM (songbook.viewsongs 
      INNER JOIN songbook.songperformances 
      ON (((viewsongs.ID = songperformances.Song) AND ((viewsongs.Code <> 'A') OR (viewsongs.Code IS NULL)))))
GO
